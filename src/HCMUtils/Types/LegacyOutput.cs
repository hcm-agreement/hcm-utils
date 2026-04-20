namespace HCMUtils.Types;

public record LegacyOutput<TLegacyInput>(
    TLegacyInput InputString,
    string VersionNumber,
    (double Long, double Lat) CalculatedTxCoordinates,
    (double Long, double Lat) CalculatedRxCoordinates,
    InfoValues InfoValues
) where TLegacyInput : LegacyInput;
