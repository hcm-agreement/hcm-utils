using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HCMUtils.Tests")]

namespace HCMUtils.MultiCalc;

public class Helpers
{
    public static IEnumerable<Input> ParseMultiCalc(IEnumerable<string> inputLines)
    {
        var groupedInputLines = GroupInputLines(inputLines);

        return from groupedInput in groupedInputLines
               select (Input)(StringHelpers.ParseModeType(groupedInput[0]) == Types.ModeType.PointToLine ? ParsePointToLineMultiCalc([.. groupedInput]) : ParsePointToPointMultiCalc([.. groupedInput]));
    }

    protected static List<List<string>> GroupInputLines(IEnumerable<string> inputLines)
    {
        var groupedInputLines = new List<List<string>>();

        while (inputLines.Any())
        {
            var mode = inputLines.First();
            var takeLines = StringHelpers.ParseModeType(mode) == Types.ModeType.PointToLine ? 21 : 33;

            groupedInputLines = [.. groupedInputLines, [.. inputLines.Take(takeLines)]];

            inputLines = inputLines.Skip(takeLines);
        }

        return groupedInputLines;
    }

    internal static PointToLineInput ParsePointToLineMultiCalc(List<string> inputList) => new(
            int.Parse(inputList[0], CultureInfo.InvariantCulture),
            StringHelpers.ParseCoordinates(inputList[1]),
            int.TryParse(inputList[2], out var txHeightAboveSeaLevel) ? txHeightAboveSeaLevel : null,
            (inputList[3], inputList[4]),
            double.Parse(inputList[5], CultureInfo.InvariantCulture),
            double.Parse(inputList[6], CultureInfo.InvariantCulture),
            int.Parse(inputList[7], CultureInfo.InvariantCulture),
            double.Parse(inputList[8], CultureInfo.InvariantCulture),
            StringHelpers.ParseGainType(inputList[9]),
            StringHelpers.ParseSINumber(inputList[10]),
            StringHelpers.ParseBoolean(inputList[11]),
            inputList[12].Trim().Length > 0 ? StringHelpers.ParseTemperature(inputList[12]) : null,
            double.TryParse(inputList[13], out var distanceOverSea) ? distanceOverSea : null,
            int.Parse(inputList[14], CultureInfo.InvariantCulture),
            int.Parse(inputList[15], CultureInfo.InvariantCulture),
            ITUHelpers.ParseCountry(inputList[16]),
            ITUHelpers.ParseCountry(inputList[17]),
            double.TryParse(inputList[18], out var permissibleFieldStrength) ? permissibleFieldStrength : null,
            int.Parse(inputList[19], CultureInfo.InvariantCulture),
            inputList[20]
        );

    internal static PointToPointInput ParsePointToPointMultiCalc(List<string> inputList) => new(
            int.Parse(inputList[0], CultureInfo.InvariantCulture),
            StringHelpers.ParseCoordinates(inputList[1]),
            int.TryParse(inputList[2], out var txHeightAboveSeaLevel) ? txHeightAboveSeaLevel : null,
            (inputList[3], inputList[4]),
            double.Parse(inputList[5], CultureInfo.InvariantCulture),
            double.Parse(inputList[6], CultureInfo.InvariantCulture),
            int.Parse(inputList[7], CultureInfo.InvariantCulture),
            double.Parse(inputList[8], CultureInfo.InvariantCulture),
            StringHelpers.ParseGainType(inputList[9]),
            StringHelpers.ParseSINumber(inputList[10]),
            StringHelpers.ParseBoolean(inputList[11]),
            inputList[12].Trim().Length > 0 ? StringHelpers.ParseTemperature(inputList[12]) : null,
            double.TryParse(inputList[13], out var distanceOverSea) ? distanceOverSea : null,
            int.Parse(inputList[14], CultureInfo.InvariantCulture),
            StringHelpers.ParseCoordinates(inputList[15]),
            int.TryParse(inputList[16], out var rxHeightAboveSeaLevel) ? rxHeightAboveSeaLevel : null,
            int.Parse(inputList[17], CultureInfo.InvariantCulture),
            ITUHelpers.ParseCountry(inputList[18]),
            ITUHelpers.ParseCountry(inputList[19]),
            double.TryParse(inputList[20], out var permissibleFieldStrength) ? permissibleFieldStrength : null,
            StringHelpers.ParseSINumber(inputList[21]),
            inputList[22],
            inputList[23],
            (inputList[24], inputList[25]),
            double.Parse(inputList[26], CultureInfo.InvariantCulture),
            double.Parse(inputList[27], CultureInfo.InvariantCulture),
            StringHelpers.ParseGainType(inputList[28]),
            double.Parse(inputList[29], CultureInfo.InvariantCulture),
            double.Parse(inputList[30], CultureInfo.InvariantCulture),
            int.TryParse(inputList[31], out var frequencyDifferenceCorrectionFactor) ? frequencyDifferenceCorrectionFactor : null,
            int.Parse(inputList[32], CultureInfo.InvariantCulture)
        );
}
