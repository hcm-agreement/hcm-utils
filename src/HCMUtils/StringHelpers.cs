namespace HCMUtils;

using System.Globalization;
using System.Text.RegularExpressions;
using FluentValidation;
using HCMUtils.Types;
using HCMUtils.Validators;

public partial class StringHelpers
{
    private static readonly string[] PositiveDMSDirections = ["N", "E"];

    [GeneratedRegex(@"(?<degrees>\d{1,3})(?<direction>N|E|S|W)(?<minutes>\d{2})(?<seconds>\d{2})")]
    internal static partial Regex DMSRegex();
    /// <summary>
    /// Parses a string in the degrees-minutes-seconds format (`DD(N|E|S|W)MMSS`) used in HCM into a decimal representation.
    /// </summary>
    /// <param name="input">A DMS string, e.g. `51E2346`.</param>
    /// <returns>The decimal form of the DMS string.</returns>
    public static double ParseDMSString(string input)
    {
        var match = DMSRegex().Match(input);

        return (PositiveDMSDirections.Contains(match.Groups["direction"].Value) ? 1 : -1) * (
            double.Parse(match.Groups["degrees"].Value, CultureInfo.InvariantCulture)
            + (double.Parse(match.Groups["minutes"].Value, CultureInfo.InvariantCulture) / 60)
            + (double.Parse(match.Groups["seconds"].Value, CultureInfo.InvariantCulture) / (60 * 60))
        );
    }

    [GeneratedRegex(@"(?<long>\d{1,2}(E|W)\d{4})(?<lat>\d{1,2}(N|S)\d{4})")]
    internal static partial Regex CoordinatesRegex();
    /// <summary>
    /// Parses a string with latitude and longitude parts in DMS format used in HCM into a tuple with decimal coordinates.
    /// </summary>
    /// <param name="input">A string with latitude and longitude components in DMS format, e.g. 8E123451N4223</param>
    /// <returns>A tuple of the latitude and longitude in decimal form.</returns>
    public static (double Long, double Lat) ParseCoordinates(string input)
    {
        var match = CoordinatesRegex().Match(input);

        return (
            ParseDMSString(match.Groups["long"].Value),
            ParseDMSString(match.Groups["lat"].Value)
        );
    }

    [GeneratedRegex(@"(?<number>\d+\.\d+)(?<prefix>M|G|k)")]
    internal static partial Regex SINumberRegex();
    /// <summary>
    /// Parses a number with a SI prefix appended, e.g. 145.5M is 145500000.
    /// Its confusingly called a prefix though it follows the number as usually a unit is prefixed with it.
    /// Only Mega, Giga and kilo are supported.
    /// </summary>
    /// <param name="input">A string representing the number to parse</param>
    /// <returns>The number as a double</returns>
    public static double ParseSINumber(string input)
    {
        var match = SINumberRegex().Match(input);
        if (!match.Success)
        {
            throw new ArgumentException($"Unable to parse input: `{input}`");
        }

        return double.Parse(match.Groups["number"].Value, CultureInfo.InvariantCulture) * GetSIMultiplier(ParseSIPrefix(match.Groups["prefix"].Value));
    }

    /// <summary>
    /// Parses an input si prefix and returns an enum value
    /// </summary>
    /// <param name="input">an input string, such as `"G"`</param>
    /// <returns>The corresponding enum value, e.g. `SIPrefix.G`</returns>
    /// <exception cref="ArgumentException"></exception>
    public static SIPrefix ParseSIPrefix(string input) => input switch
    {
        "G" => SIPrefix.G,
        "M" => SIPrefix.M,
        "k" => SIPrefix.k,
        _ => throw new ArgumentException($"Unable to parse SI prefix `{input}")
    };

    /// <summary>
    /// Returns the corresponding multiplier for an SIPrefix
    /// </summary>
    /// <param name="input">The input, such as `SIPrefix.k`</param>
    /// <returns>The corresponding multiplier, such as `1_000`</returns>
    /// <exception cref="ArgumentException"></exception>
    public static int GetSIMultiplier(SIPrefix input) => input switch
    {
        SIPrefix.G => 1_000_000_000,
        SIPrefix.M => 1_000_000,
        SIPrefix.k => 1_000,
        _ => throw new ArgumentException($"Not an enum value `{input}`")
    };

    /// <summary>
    /// Parses a 0/N or 1/Y to its respective boolean (1/Y = true, everything else = false)
    /// </summary>
    /// <param name="input">A string containing either a 0 or 1 or N or Y</param>
    /// <returns>The corresponding boolean</returns>
    public static bool ParseBoolean(string input)
    {
        var normalizedInput = input.Trim();

        return normalizedInput.Equals("1", StringComparison.OrdinalIgnoreCase) || normalizedInput.Equals("y", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Parses a temperature string into a temperature.
    /// </summary>
    /// <param name="input">The input string, either "W" or "C" for warm or cold.</param>
    /// <returns>The parsed temperature</returns>
    public static Temperature ParseTemperature(string input) => input.Trim().ToLower(CultureInfo.InvariantCulture) switch
    {
        "w" => Temperature.Warm,
        "c" => Temperature.Cold,
        _ => throw new ArgumentException($"Unable to parse temperature `{input}")
    };

    /// <summary>
    /// Get the mode type from a string.
    /// </summary>
    /// <param name="input">A string containing a mode, e.g. -10 or 1</param>
    /// <returns>The mode type corresponding with the mode described by the string</returns>
    public static ModeType ParseModeType(string input) => int.Parse(input, CultureInfo.InvariantCulture) >= 0 ? ModeType.PointToPoint : ModeType.PointToLine;

    /// <summary>
    /// Get the gain type of a string
    /// </summary>
    /// <param name="input">A string, either "E" or "I"</param>
    /// <returns>The corresponding gain type</returns>
    /// <exception cref="Exception"></exception>
    public static GainType ParseGainType(string input) => input.Trim().ToLower(CultureInfo.InvariantCulture) switch
    {
        "e" => GainType.Dipole,
        "i" => GainType.Isotropic,
        _ => throw new ArgumentException($"Unable to parse gain type `{input}")
    };

    /// <summary>
    /// Get a DMS string from a decimal degree number and the axis (lat or long)
    /// </summary>
    /// <param name="degrees">A coordinate in decimal degree form</param>
    /// <param name="isLatitude">Whether the decimal is on the latitudinal or longitudinal axis</param>
    /// <returns></returns>
    public static string ToDMSString(double degrees, bool isLatitude)
    {
        var degreesPart = Math.Truncate(Math.Abs(degrees));
        var minutesPart = (Math.Abs(degrees) - degreesPart) * 60;
        var secondsPart = (minutesPart - Math.Truncate(minutesPart)) * 60;

        var direction = isLatitude ? (
            degrees >= 0 ? 'N' : 'S'
        ) : (
            degrees >= 0 ? 'E' : 'W'
        );

        return degreesPart.ToString(CultureInfo.InvariantCulture).PadLeft(isLatitude ? 2 : 3, '0')
            + direction
            + Math.Truncate(minutesPart).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')
            + Math.Round(secondsPart).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
    }

    /// <summary>
    /// Get a coordinates string from a latitude and longitude in tupel form
    /// </summary>
    /// <param name="coordinates">A tuple of coordinates</param>
    /// <returns>The string representation in DMS form of the coordinate pair</returns>
    public static string ToCoordinatesString((double Long, double Lat) coordinates) => ToDMSString(coordinates.Long, false) + ToDMSString(coordinates.Lat, true);

    /// <summary>
    /// Get a E/I char from a GainType
    /// </summary>
    /// <param name="gainType">The input GainType</param>
    /// <returns>E for dipole-based gain calculation, I for isotropic radiator-based gain calculation</returns>
    public static char ToGainTypeString(GainType gainType) => gainType == GainType.Dipole ? 'E' : 'I';

    /// <summary>
    /// Returns the associated SI prefix string
    /// </summary>
    /// <param name="prefix">an SIPrefix</param>
    /// <returns>the string representation of the input, e.g. `"G"`</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string ToSIPrefixString(SIPrefix prefix) => prefix switch
    {
        SIPrefix.G => "G",
        SIPrefix.M => "M",
        SIPrefix.k => "k",
        _ => throw new ArgumentException($"Not an enum value `{prefix}`")
    };

    /// <summary>
    /// Converts an input frequency as a number into a string frequency with a desired prefix
    /// </summary>
    /// <param name="input">The input frequency to convert, e.g. 3_750_000_000</param>
    /// <param name="desiredPrefix">The desired prefix, e.g. G</param>
    /// <returns>The frequency as a string including the desired prefix, e.g. 3.75000G</returns>
    public static string ToFrequencyString(double input, SIPrefix desiredPrefix) => (input / GetSIMultiplier(desiredPrefix)).ToString("#####.00000", CultureInfo.InvariantCulture) +
        ToSIPrefixString(desiredPrefix);

    /// <summary>
    /// Converts a boolean to a string
    /// </summary>
    /// <param name="input">The input boolean, such as true</param>
    /// <returns>The ouput string, e.g. "1"</returns>
    public static string ToBooleanString(bool input) => input ? "1" : "0";

    public static string ToTemperatureString(Temperature temperature) => temperature == Temperature.Warm ? "W" : "C";

    /// <summary>
    /// Returns a legacy string for point-to-point calculations
    /// </summary>
    /// <param name="input">The input data to build the string</param>
    /// <returns>The legacy string</returns>
    public static string ToLegacyString(
        LegacyPointToPointInput input
    )
    {
        new LegacyPointToPointInputValidator().Validate(input, options =>
        {
            options.IncludeAllRuleSets();
            options.ThrowOnFailures();
        });

        return ToCoordinatesString(input.TxCoordinates) +
            ToCoordinatesString(input.RxCoordinates) +
            (input.TxSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
            (input.RxSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
            input.TxAntennaType.Horizontal.PadLeft(7) +
            input.TxAntennaType.Vertical.PadLeft(7) +
            input.TxAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
            input.TxElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
            input.TxAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
            input.RxAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
            ToGainTypeString(input.TxGainType) +
            input.TxPower.ToString("###.00", CultureInfo.InvariantCulture).PadLeft(6) +
            ToFrequencyString(input.TxFrequency, SIPrefix.M).PadLeft(12) +
            ToBooleanString(input.ChannelOccupation) +
            (input.SeaTemperature == null ? " " : ToTemperatureString((Temperature)input.SeaTemperature)) + // waiting until they fixed dotnet/csharplang#33
            input.TxServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
            input.RxServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
            (input.DistanceOverSea?.ToString(CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
            ToFrequencyString(input.RxFrequency, SIPrefix.M).PadLeft(12) +
            input.RxEmissionDesignation.PadLeft(9) +
            input.TxEmissionDesignation.PadLeft(9) +
            input.RxAntennaType.Horizontal.PadLeft(7) +
            input.RxAntennaType.Vertical.PadLeft(7) +
            input.RxAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
            input.RxElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
            ToGainTypeString(input.RxGainType) +
            input.RxGain.ToString("##.0", CultureInfo.InvariantCulture).PadLeft(4) +
            input.DepolarizationLoss.ToString("##.0", CultureInfo.InvariantCulture).PadLeft(4) +
            (input.PermissibleFieldStrength?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
            (input.FrequencyDifferenceCorrectionFactor?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
            ITUHelpers.ToITULetterCodeString(input.RxCountry).PadRight(3, '_') +
            ITUHelpers.ToITULetterCodeString(input.TxCountry).PadRight(3, '_') +
            "".PadLeft(3) +
            input.TopoPath.PadRight(63) +
            input.BorderPath.PadRight(63) +
            input.MorphoPath.PadRight(63) +
            "".PadLeft(6) +
            "".PadLeft(20) +
            "".PadLeft(15) +
            "".PadLeft(15) +
            input.DebugOutputPath;
    }

    /// <summary>
    /// Returns a legacy string for point-to-line calculations
    /// </summary>
    /// <param name="input">The input data to build the string</param>
    /// <returns>The legacy string</returns>
    public static string ToLegacyString(
        LegacyPointToLineInput input
    )
    {
        new LegacyPointToLineInputValidator().Validate(input, options =>
        {
            options.IncludeAllRuleSets();
            options.ThrowOnFailures();
        });

        return ToCoordinatesString(input.TxCoordinates) +
          "".PadLeft(15) +
          (input.TxSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
          "".PadLeft(4) +
          input.TxAntennaType.Horizontal.PadLeft(7) +
          input.TxAntennaType.Vertical.PadLeft(7) +
          input.TxAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          input.TxElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          input.TxAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
          "".PadLeft(4) +
          ToGainTypeString(input.TxGainType) +
          input.TxPower.ToString("###.00", CultureInfo.InvariantCulture).PadLeft(6) +
          ToFrequencyString(input.TxFrequency, SIPrefix.M).PadLeft(12) +
          ToBooleanString(input.ChannelOccupation) +
          (input.SeaTemperature == null ? " " : ToTemperatureString((Temperature)input.SeaTemperature)) + // waiting until they fixed dotnet/csharplang#33
          input.TxServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
          "".PadLeft(5) +
          (input.DistanceOverSea?.ToString(CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          "".PadLeft(12) +
          "".PadLeft(9) +
          input.TxEmissionDesignation.PadLeft(9) +
          "".PadLeft(14) +
          "".PadLeft(5) +
          "".PadLeft(5) +
          " " +
          "".PadLeft(4) +
          "".PadLeft(4) +
          (input.PermissibleFieldStrength?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          "".PadLeft(4) +
          ITUHelpers.ToITULetterCodeString(input.TargetCountry).PadRight(3, '_') +
          ITUHelpers.ToITULetterCodeString(input.TxCountry).PadRight(3, '_') +
          input.MaxCrossBorderRange.ToString(CultureInfo.InvariantCulture).PadLeft(3) +
          input.TopoPath.PadRight(63) +
          input.BorderPath.PadRight(63) +
          input.MorphoPath.PadRight(63) +
          "".PadLeft(6) +
          "".PadLeft(20) +
          "".PadLeft(15) +
          "".PadLeft(15) +
          input.DebugOutputPath;
    }

    [GeneratedRegex(@"(?<txSiteHeightFromDatabase>F|T)(?<txSiteHeightDifferentFromDatabase>F|T)(?<txSiteHeightLargeDifferenceFromDatabase>F|T)(?<frequencyOutOfRange>F|T)(?<permissibleFieldStrengthInputUsed>F|T)(?<maxCrossBorderRangeInputUsed>F|T)(?<serviceAreasOverlapping>F|T)(?<rxSiteHeightFromDatabase>F|T)(?<rxSiteHeightDifferentFromDatabase>F|T)(?<rxSiteHeightLargeDifferenceFromDatabase>F|T)(?<freeSpaceFieldStrengthUsedBecauseSmallDistance>F|T)(?<freeSpaceFieldStrengthUsedBecauseFirstFresnelZoneFree>F|T)(?<distanceOverSeaLargerThanDistanceBetweenTxRx>F|T)(?<frequencyDifferenceCorrectionFactorInputUsed>F|T)(?<frequencyDifferenceOutOfRange>F|T)(?<distanceOverSeaReset>F|T)(?<txChannelSpacingOutOfRange>F|T)(?<correctionFactors380400Used>F|T)")]
    internal static partial Regex InfoValuesRegex();

    /// <summary>
    /// Parses a string of info values
    /// </summary>
    /// <param name="input">A string of at least 18 characters being either `T` or `F`</param>
    /// <returns>The parsed info values</returns>
    /// <exception cref="ArgumentException"></exception>
    public static InfoValues ParseInfoValues(string input)
    {
        var match = InfoValuesRegex().Match(input.ToUpperInvariant());
        if (!match.Success)
        {
            throw new ArgumentException($"Unable to parse input: `{input}`");
        }

        return new InfoValues(
            match.Groups["txSiteHeightFromDatabase"].Value == "T",
            match.Groups["txSiteHeightDifferentFromDatabase"].Value == "T",
            match.Groups["txSiteHeightLargeDifferenceFromDatabase"].Value == "T",
            match.Groups["frequencyOutOfRange"].Value == "T",
            match.Groups["permissibleFieldStrengthInputUsed"].Value == "T",
            match.Groups["maxCrossBorderRangeInputUsed"].Value == "T",
            match.Groups["serviceAreasOverlapping"].Value == "T",
            match.Groups["rxSiteHeightFromDatabase"].Value == "T",
            match.Groups["rxSiteHeightDifferentFromDatabase"].Value == "T",
            match.Groups["rxSiteHeightLargeDifferenceFromDatabase"].Value == "T",
            match.Groups["freeSpaceFieldStrengthUsedBecauseSmallDistance"].Value == "T",
            match.Groups["freeSpaceFieldStrengthUsedBecauseFirstFresnelZoneFree"].Value == "T",
            match.Groups["distanceOverSeaLargerThanDistanceBetweenTxRx"].Value == "T",
            match.Groups["frequencyDifferenceCorrectionFactorInputUsed"].Value == "T",
            match.Groups["frequencyDifferenceOutOfRange"].Value == "T",
            match.Groups["distanceOverSeaReset"].Value == "T",
            match.Groups["txChannelSpacingOutOfRange"].Value == "T",
            match.Groups["correctionFactors380400Used"].Value == "T"
        );
    }

    /// <summary>
    /// Parses a legacy output string, including the calculated tx/rx coordinates, version and info values
    /// </summary>
    /// <param name="outputString"></param>
    /// <returns>The parsed output string including the input it was read out of</returns>
    public static LegacyOutput ParseLegacyOutputString(string outputString)
    {
        var input = ParseLegacyInputString(outputString);

        return new LegacyOutput(
            input,
            outputString[376..382].Trim(),
            ParseCoordinates(outputString[402..417]),
            ParseCoordinates(outputString[417..432]),
            ParseInfoValues(outputString[382..402])
        );
    }

    /// <summary>
    /// Parses a legacy input string
    /// </summary>
    /// <param name="input">The legacy input string</param>
    /// <returns>A legacy input containing the parsed values</returns>
    public static LegacyInput ParseLegacyInputString(string input)
    {
        // TODO validate

        if (input[15..31].Trim().Length == 0)
        {
            return new LegacyPointToLineInput(
                ParseCoordinates(input[..15]),
                int.TryParse(input[30..34], CultureInfo.InvariantCulture, out var txSiteHeight) ? txSiteHeight : null,
                (input[38..45], input[45..52]),
                double.Parse(input[52..57], CultureInfo.InvariantCulture),
                double.Parse(input[57..62], CultureInfo.InvariantCulture),
                int.Parse(input[62..66], CultureInfo.InvariantCulture),
                ParseGainType(input[70..71]),
                double.Parse(input[71..77], CultureInfo.InvariantCulture),
                ParseSINumber(input[77..89]),
                ParseBoolean(input[89..90]),
                input[90..91].Trim().Length > 0 ? ParseTemperature(input[90..91]) : null,
                int.Parse(input[91..96], CultureInfo.InvariantCulture),
                int.TryParse(input[101..106], CultureInfo.InvariantCulture, out var distanceOverSea) ? distanceOverSea : null,
                input[127..136],
                double.TryParse(input[169..174], CultureInfo.InvariantCulture, out var permissibleFieldStrength) ? permissibleFieldStrength : null,
                ITUHelpers.ParseCountry(input[178..181]),
                ITUHelpers.ParseCountry(input[181..184]),
                int.Parse(input[184..187], CultureInfo.InvariantCulture),
                input[187..250].Trim(),
                input[250..313].Trim(),
                input[313..376].Trim(),
                input[432..].Trim().Length > 0 ? input[432..].Trim() : null
            );
        }
        else
        {
            return new LegacyPointToPointInput(
                ParseCoordinates(input[..15]),
                ParseCoordinates(input[15..30]),
                int.TryParse(input[30..34], CultureInfo.InvariantCulture, out var txSiteHeight) ? txSiteHeight : null,
                int.TryParse(input[34..38], CultureInfo.InvariantCulture, out var rxSiteHeight) ? rxSiteHeight : null,
                (input[38..45], input[45..52]),
                double.Parse(input[52..57], CultureInfo.InvariantCulture),
                double.Parse(input[57..62], CultureInfo.InvariantCulture),
                int.Parse(input[62..66], CultureInfo.InvariantCulture),
                int.Parse(input[66..70], CultureInfo.InvariantCulture),
                ParseGainType(input[70..71]),
                double.Parse(input[71..77], CultureInfo.InvariantCulture),
                ParseSINumber(input[77..89]),
                ParseBoolean(input[89..90]),
                input[90..91].Trim().Length > 0 ? ParseTemperature(input[90..91]) : null,
                int.Parse(input[91..96], CultureInfo.InvariantCulture),
                int.Parse(input[96..101], CultureInfo.InvariantCulture),
                int.TryParse(input[101..106], CultureInfo.InvariantCulture, out var distanceOverSea) ? distanceOverSea : null,
                ParseSINumber(input[106..118]),
                input[118..127],
                input[127..136],
                (input[136..143], input[143..150]),
                double.Parse(input[150..155], CultureInfo.InvariantCulture),
                double.Parse(input[155..160], CultureInfo.InvariantCulture),
                ParseGainType(input[160..161]),
                double.Parse(input[161..165], CultureInfo.InvariantCulture),
                double.Parse(input[165..169], CultureInfo.InvariantCulture),
                double.TryParse(input[169..174], CultureInfo.InvariantCulture, out var permissibleFieldStrength) ? permissibleFieldStrength : null,
                int.TryParse(input[174..178], CultureInfo.InvariantCulture, out var frequencyDifferenceCorrectionFactor) ? frequencyDifferenceCorrectionFactor : null,
                ITUHelpers.ParseCountry(input[178..181]),
                ITUHelpers.ParseCountry(input[181..184]),
                input[187..250].Trim(),
                input[250..313].Trim(),
                input[313..376].Trim(),
                input[432..].Trim().Length > 0 ? input[432..].Trim() : null
            );
        }

    }
}
