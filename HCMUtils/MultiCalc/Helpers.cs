using System.Runtime.CompilerServices;
using HCMUtils.String;

[assembly: InternalsVisibleTo("HCMUtilsTests")]

namespace HCMUtils.MultiCalc;

public class Helpers
{
    public static IEnumerable<Input> ParseMultiCalc(IEnumerable<string> inputLines)
    {
        var groupedInputLines = GroupInputLines(inputLines);

        return from groupedInput in groupedInputLines
               select (Input)(String.Helpers.ParseModeType(groupedInput[0]) == Types.ModeType.PointToLine ? ParsePointToLineMultiCalc([.. groupedInput]) : ParsePointToPointMultiCalc([.. groupedInput]));
    }

    static List<List<string>> GroupInputLines(IEnumerable<string> inputLines)
    {
        var groupedInputLines = new List<List<string>>();

        while (inputLines.Any())
        {
            var mode = inputLines.First();
            var takeLines = String.Helpers.ParseModeType(mode) == Types.ModeType.PointToLine ? 21 : 33;

            groupedInputLines = [.. groupedInputLines, [.. inputLines.Take(takeLines)]];

            inputLines = inputLines.Skip(takeLines);
        }

        return groupedInputLines;
    }

    internal static PointToLineInput ParsePointToLineMultiCalc(List<string> inputList)
    {
        return new(
            int.Parse(inputList[0]),
            String.Helpers.ParseCoordinates(inputList[1]),
            int.TryParse(inputList[2], out var TxHeightAboveSeaLevel) ? TxHeightAboveSeaLevel : null,
            (inputList[3], inputList[4]),
            double.Parse(inputList[5]),
            double.Parse(inputList[6]),
            int.Parse(inputList[7]),
            double.Parse(inputList[8]),
            String.Helpers.ParseGainType(inputList[9]),
            String.Helpers.ParseSINumber(inputList[10]),
            String.Helpers.ParseBoolean(inputList[11]),
            inputList[12].Trim().Length > 0 ? String.Helpers.ParseTemperature(inputList[12]) : null,
            double.TryParse(inputList[13], out var DistanceOverSea) ? DistanceOverSea : null,
            int.Parse(inputList[14]),
            int.Parse(inputList[15]),
            ITUHelpers.ParseITULetterCode(inputList[16]),
            ITUHelpers.ParseITULetterCode(inputList[17]),
            double.TryParse(inputList[18], out var PermissibleFieldStrength) ? PermissibleFieldStrength : null,
            int.Parse(inputList[19])
        );
    }

    internal static PointToPointInput ParsePointToPointMultiCalc(List<string> inputList)
    {
        return new(
            int.Parse(inputList[0]),
            String.Helpers.ParseCoordinates(inputList[1]),
            int.TryParse(inputList[2], out var TxHeightAboveSeaLevel) ? TxHeightAboveSeaLevel : null,
            (inputList[3], inputList[4]),
            double.Parse(inputList[5]),
            double.Parse(inputList[6]),
            int.Parse(inputList[7]),
            double.Parse(inputList[8]),
            String.Helpers.ParseGainType(inputList[9]),
            String.Helpers.ParseSINumber(inputList[10]),
            String.Helpers.ParseBoolean(inputList[11]),
            inputList[12].Trim().Length > 0 ? String.Helpers.ParseTemperature(inputList[12]) : null,
            double.TryParse(inputList[13], out var DistanceOverSea) ? DistanceOverSea : null,
            int.Parse(inputList[14]),
            String.Helpers.ParseCoordinates(inputList[15]),
            int.TryParse(inputList[16], out var RxHeightAboveSeaLevel) ? RxHeightAboveSeaLevel : null,
            int.Parse(inputList[17]),
            ITUHelpers.ParseITULetterCode(inputList[18]),
            ITUHelpers.ParseITULetterCode(inputList[19]),
            double.TryParse(inputList[20], out var PermissibleFieldStrength) ? PermissibleFieldStrength : null,
            String.Helpers.ParseSINumber(inputList[21]),
            inputList[22],
            inputList[23],
            (inputList[24], inputList[25]),
            double.Parse(inputList[26]),
            double.Parse(inputList[27]),
            String.Helpers.ParseGainType(inputList[28]),
            double.Parse(inputList[29]),
            double.Parse(inputList[30]),
            int.TryParse(inputList[31], out var FrequencyDifferenceCorrectionFactor) ? FrequencyDifferenceCorrectionFactor : null,
            int.Parse(inputList[32])
        );
    }
}