namespace HCMUtils.Types;

public record LegacyOutput(
    LegacyInput Input,
    Temperature SeaTemperature,
    string VersionNumber,
    (double Long, double Lat) CalculatedTxCoordinates,
    (double Long, double Lat) CalculatedRxCoordinates,
    InfoValues InfoValues
);
