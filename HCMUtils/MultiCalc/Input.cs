using HCMUtils.Types;

namespace HCMUtils.MultiCalc;

public record Input(
    int Mode,
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
    Country TxCountry,
    double? PermissibleFieldStrength
);