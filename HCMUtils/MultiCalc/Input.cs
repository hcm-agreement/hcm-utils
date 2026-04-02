using HCMUtils.Types;

namespace HCMUtils.MultiCalc;

public record Input(
    int Mode,
    (double Lat, double Long) TxCoordinates,
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
    Country TxCountry,
    double? PermissibleFieldStrength,
    string TxEmissionDesignation
);