namespace HCMUtils.Types;

/// <summary>
/// Used as an input to StringHelpers.ToLegacyString
/// </summary>
/// <param name="TxCoordinates"></param>
/// <param name="RxCoordinates"></param>
/// <param name="TxSiteHeight"></param>
/// <param name="RxSiteHeight"></param>
/// <param name="TxAntennaType"></param>
/// <param name="TxAzimuth"></param>
/// <param name="TxElevation"></param>
/// <param name="TxAntennaHeight"></param>
/// <param name="RxAntennaHeight"></param>
/// <param name="TxGainType"></param>
/// <param name="TxPower"></param>
/// <param name="TxFrequency"></param>
/// <param name="ChannelOccupation"></param>
/// <param name="SeaTemperature"></param>
/// <param name="TxServiceAreaRadius"></param>
/// <param name="RxServiceAreaRadius"></param>
/// <param name="DistanceOverSea"></param>
/// <param name="RxFrequency"></param>
/// <param name="RxEmissionDesignation"></param>
/// <param name="TxEmissionDesignation"></param>
/// <param name="RxAntennaType"></param>
/// <param name="RxAzimuth"></param>
/// <param name="RxElevation"></param>
/// <param name="RxGainType"></param>
/// <param name="RxGain"></param>
/// <param name="DepolarizationLoss"></param>
/// <param name="PermissibleFieldStrength"></param>
/// <param name="FrequencyDifferenceCorrectionFactor"></param>
/// <param name="RxCountry"></param>
/// <param name="TxCountry"></param>
/// <param name="TopoPath">An absolute path where to find the topo data</param>
/// <param name="BorderPath">An absolute path where to find the border data</param>
/// <param name="MorphoPath">An absolute path where to find the morpho data</param>
/// <param name="DebugOutputPath">An absolute path where a debug output will be placed</param>
public record LegacyPointToPointInput(
    (double Lat, double Long) TxCoordinates,
    (double Lat, double Long) RxCoordinates,
    int? TxSiteHeight,
    int? RxSiteHeight,
    (string Horizontal, string Vertical) TxAntennaType,
    double TxAzimuth,
    double TxElevation,
    int TxAntennaHeight,
    int RxAntennaHeight,
    GainType TxGainType,
    double TxPower,
    double TxFrequency,
    bool ChannelOccupation,
    Temperature? SeaTemperature,
    int TxServiceAreaRadius,
    int RxServiceAreaRadius,
    double? DistanceOverSea,
    double RxFrequency,
    string RxEmissionDesignation,
    string TxEmissionDesignation,
    (string Horizontal, string Vertical) RxAntennaType,
    double RxAzimuth,
    double RxElevation,
    GainType RxGainType,
    double RxGain,
    double DepolarizationLoss,
    double? PermissibleFieldStrength,
    int? FrequencyDifferenceCorrectionFactor,
    Country RxCountry,
    Country TxCountry,
    string TopoPath,
    string BorderPath,
    string MorphoPath,
    string? DebugOutputPath
) : LegacyInput(
    TxCoordinates,
    TxSiteHeight,
    TxAntennaType,
    TxAzimuth,
    TxElevation,
    TxAntennaHeight,
    TxGainType,
    TxPower,
    TxFrequency,
    ChannelOccupation,
    SeaTemperature,
    TxServiceAreaRadius,
    DistanceOverSea,
    TxEmissionDesignation,
    PermissibleFieldStrength,
    TxCountry,
    TopoPath,
    BorderPath,
    MorphoPath,
    DebugOutputPath
);
