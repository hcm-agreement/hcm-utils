using System.Text.RegularExpressions;
using HCMUtils.Types;

namespace HCMUtils.String;

public partial class Helpers
{
    internal static string[] PositiveDMSDirections = ["N", "E"];

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
            double.Parse(match.Groups["degrees"].Value)
            + double.Parse(match.Groups["minutes"].Value) / 60
            + double.Parse(match.Groups["seconds"].Value) / (60 * 60)
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

        return double.Parse(match.Groups["number"].Value) * GetSIMultiplier(match.Groups["prefix"].Value);
    }

    static int GetSIMultiplier(string input)
    {
        return input switch
        {
            "G" => 1_000_000_000,
            "M" => 1_000_000,
            "k" => 1_000,
            _ => 1,
        };
    }

    /// <summary>
    /// Parses a 0/N or 1/Y to its respective boolean (1/Y = true, everything else = false)
    /// </summary>
    /// <param name="input">A string containing either a 0 or 1 or N or Y</param>
    /// <returns>The corresponding boolean</returns>
    public static bool ParseBoolean(string input)
    {
        var normalizedInput = input.Trim().ToLower();

        return normalizedInput.Equals("1") || normalizedInput.Equals("y");
    }

    /// <summary>
    /// Parses a temperature string into a temperature.
    /// </summary>
    /// <param name="input">The input string, either "W" or "C" for warm or cold.</param>
    /// <returns>The parsed temperature</returns>
    public static Temperature ParseTemperature(string input)
    {
        return input.Trim().ToLower() switch
        {
            "w" => Temperature.Warm,
            "c" => Temperature.Cold,
            _ => throw new Exception($"Unable to parse temperature `{input}")
        };
    }
    public static ModeType ParseModeType(string input)
    {
        return int.Parse(input) >= 0 ? ModeType.PointToPoint : ModeType.PointToLine;
    }

    public static GainType ParseGainType(string input)
    {
        return input.Trim().ToLower() switch
        {
            "e" => GainType.Dipole,
            "i" => GainType.Isotropic,
            _ => throw new Exception($"Unable to parse gain type `{input}")
        };
    }
}
