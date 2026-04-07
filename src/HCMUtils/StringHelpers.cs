namespace HCMUtils;

using System.Globalization;
using System.Text.RegularExpressions;
using HCMUtils.Types;

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

    public static string BuildLegacyInputString(
        (double Lat, double Long) txCoordinates,
        (double Lat, double Long) rxCoordinates,
        int? txSiteHeight,
        int? rxSiteHeight,
        (string Horizontal, string Vertical) txAntennaType,
        double txAzimuth,
        double txElevation,
        int txAntennaHeight,
        int rxAntennaHeight,
        GainType txGainType,
        double txPower,
        double txFrequency,
        bool channelOccupation,
        Temperature? seaTemperature,
        int txServiceAreaRadius,
        int rxServiceAreaRadius,
        double? distanceOverSea,
        double rxFrequency,
        string rxEmissionDesignation,
        string txEmissionDesignation,
        (string Horizontal, string Vertical) rxAntennaType,
        double rxAzimuth,
        double rxElevation,
        GainType rxGainType,
        double rxGain,
        double depolarizationLoss,
        double? permissibleFieldStrength,
        int? frequencyDifferenceCorrectionFactor,
        Country rxCountry,
        Country txCountry,
        string topoPath,
        string borderPath,
        string morphoPath,
        string? debugOutputPath
    ) => ToCoordinatesString(txCoordinates) +
          ToCoordinatesString(rxCoordinates) +
          (txSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
          (rxSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
          txAntennaType.Horizontal.PadLeft(7) +
          txAntennaType.Vertical.PadLeft(7) +
          txAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          txElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          txAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
          rxAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
          ToGainTypeString(txGainType) +
          txPower.ToString("###.00", CultureInfo.InvariantCulture).PadLeft(6) +
          ToFrequencyString(txFrequency, SIPrefix.M).PadLeft(12) +
          ToBooleanString(channelOccupation) +
          (seaTemperature == null ? " " : ToTemperatureString((Temperature)seaTemperature)) + // waiting until they fixed dotnet/csharplang#33
          txServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
          rxServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
          (distanceOverSea?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          ToFrequencyString(rxFrequency, SIPrefix.M).PadLeft(12) +
          rxEmissionDesignation.PadLeft(9) +
          txEmissionDesignation.PadLeft(9) +
          rxAntennaType.Horizontal.PadLeft(7) +
          rxAntennaType.Vertical.PadLeft(7) +
          rxAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          rxElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          ToGainTypeString(rxGainType) +
          rxGain.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(4) +
          depolarizationLoss.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(4) +
          (permissibleFieldStrength?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          (frequencyDifferenceCorrectionFactor?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
          ITUHelpers.ToITULetterCodeString(rxCountry).PadRight(3, '_') +
          ITUHelpers.ToITULetterCodeString(txCountry).PadRight(3, '_') +
          "".PadLeft(3) +
          topoPath.PadRight(63) +
          borderPath.PadRight(63) +
          morphoPath.PadRight(63) +
          "".PadLeft(6) +
          "".PadLeft(20) +
          "".PadLeft(15) +
          "".PadLeft(15) +
          debugOutputPath;

    public static string BuildLegacyInputString(
        (double Lat, double Long) txCoordinates,
        int? txSiteHeight,
        (string Horizontal, string Vertical) txAntennaType,
        double txAzimuth,
        double txElevation,
        int txAntennaHeight,
        GainType txGainType,
        double txPower,
        double txFrequency,
        bool channelOccupation,
        Temperature? seaTemperature,
        int txServiceAreaRadius,
        double? distanceOverSea,
        string txEmissionDesignation,
        double? permissibleFieldStrength,
        Country targetCountry,
        Country txCountry,
        int maxCrossBorderRange,
        string topoPath,
        string borderPath,
        string morphoPath,
        string? debugOutputPath
    ) => ToCoordinatesString(txCoordinates) +
          "".PadLeft(15) +
          (txSiteHeight?.ToString(CultureInfo.InvariantCulture).PadLeft(4) ?? "    ") +
          "".PadLeft(4) +
          txAntennaType.Horizontal.PadLeft(7) +
          txAntennaType.Vertical.PadLeft(7) +
          txAzimuth.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          txElevation.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) +
          txAntennaHeight.ToString(CultureInfo.InvariantCulture).PadLeft(4) +
          "".PadLeft(4) +
          ToGainTypeString(txGainType) +
          txPower.ToString("###.00", CultureInfo.InvariantCulture).PadLeft(6) +
          ToFrequencyString(txFrequency, SIPrefix.M).PadLeft(12) +
          ToBooleanString(channelOccupation) +
          (seaTemperature == null ? " " : ToTemperatureString((Temperature)seaTemperature)) + // waiting until they fixed dotnet/csharplang#33
          txServiceAreaRadius.ToString(CultureInfo.InvariantCulture).PadLeft(5) +
          "".PadLeft(5) +
          (distanceOverSea?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          "".PadLeft(12) +
          "".PadLeft(9) +
          txEmissionDesignation.PadLeft(9) +
          "".PadLeft(14) +
          "".PadLeft(5) +
          "".PadLeft(5) +
          " " +
          "".PadLeft(4) +
          "".PadLeft(4) +
          (permissibleFieldStrength?.ToString("###.0", CultureInfo.InvariantCulture).PadLeft(5) ?? "     ") +
          "".PadLeft(4) +
          ITUHelpers.ToITULetterCodeString(targetCountry).PadRight(3, '_') +
          ITUHelpers.ToITULetterCodeString(txCountry).PadRight(3, '_') +
          maxCrossBorderRange.ToString(CultureInfo.InvariantCulture).PadLeft(3) +
          topoPath.PadRight(63) +
          borderPath.PadRight(63) +
          morphoPath.PadRight(63) +
          "".PadLeft(6) +
          "".PadLeft(20) +
          "".PadLeft(15) +
          "".PadLeft(15) +
          debugOutputPath;
}
