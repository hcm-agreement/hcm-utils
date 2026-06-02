namespace HCMUtils.MultiCalc;

using HCMUtils.Types;

public record PointToLineInput(
    int Mode,
    (double Long, double Lat) TxCoordinates,
    int? TxSiteHeight,
    (string Horizontal, string Vertical) TxAntennaType,
    double TxAzimuth,
    double TxElevation,
    int TxAntennaHeight,
    double TxPower,
    GainType TxGainType,
    double TxFrequency,
    bool ChannelOccupation,
    Temperature? SeaTemperature,
    double? DistanceOverSea,
    int TxServiceAreaRadius,
    int DistanceToBorderline,
    Country TxCountry,
    Country TargetCountry,
    double? PermissibleFieldStrength,
    int MaxCrossBorderRange,
    string TxEmissionDesignation
) : Input(
        Mode,
        TxCoordinates,
        TxSiteHeight,
        TxAntennaType,
        TxAzimuth,
        TxElevation,
        TxAntennaHeight,
        TxPower,
        TxGainType,
        TxFrequency,
        ChannelOccupation,
        SeaTemperature,
        DistanceOverSea,
        TxServiceAreaRadius,
        TxCountry,
        PermissibleFieldStrength,
        TxEmissionDesignation
    );
