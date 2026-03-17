using HCMUtils.Types;

namespace HCMUtils.MultiCalc;

public record PointToLineInput(
    (double Lat, double Long) TxCoordinates,
    int? TxHeightAboveSeaLevel,
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
    int TxServiceAreaReadius,
    int DistanceToBorderline,
    Country TxCountry,
    Country TargetCountry,
    double? PermissibleFieldStrength,
    int MaxCrossBorderRange
) : Input(
        TxCoordinates,
        TxHeightAboveSeaLevel,
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
        TxServiceAreaReadius,
        TxCountry,
        PermissibleFieldStrength
    );