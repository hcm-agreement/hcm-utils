namespace HCMUtils.Types;

/// <summary>
/// Used as an input to StringHelpers.ToLegacyString
/// </summary>
/// <param name="TxCoordinates"></param>
/// <param name="TxSiteHeight"></param>
/// <param name="TxAntennaType"></param>
/// <param name="TxAzimuth"></param>
/// <param name="TxElevation"></param>
/// <param name="TxAntennaHeight"></param>
/// <param name="TxGainType"></param>
/// <param name="TxPower"></param>
/// <param name="TxFrequency"></param>
/// <param name="ChannelOccupation"></param>
/// <param name="SeaTemperature"></param>
/// <param name="TxServiceAreaRadius"></param>
/// <param name="DistanceOverSea"></param>
/// <param name="TxEmissionDesignation"></param>
/// <param name="PermissibleFieldStrength"></param>
/// <param name="TargetCountry"></param>
/// <param name="TxCountry"></param>
/// <param name="MaxCrossBorderRange"></param>
/// <param name="TopoPath">An absolute path where to find the topo data</param>
/// <param name="BorderPath">An absolute path where to find the border data</param>
/// <param name="MorphoPath">An absolute path where to find the morpho data</param>
/// <param name="DebugOutputPath">An absolute path where a debug output will be placed</param>
public record LegacyInput(
    (double Long, double Lat) TxCoordinates,
    int? TxSiteHeight,
    (string Horizontal, string Vertical) TxAntennaType,
    double TxAzimuth,
    double TxElevation,
    int TxAntennaHeight,
    GainType TxGainType,
    double TxPower,
    double TxFrequency,
    bool ChannelOccupation,
    Temperature? SeaTemperature,
    int TxServiceAreaRadius,
    int? DistanceOverSea,
    string TxEmissionDesignation,
    double? PermissibleFieldStrength,
    Country TxCountry,
    string TopoPath,
    string BorderPath,
    string MorphoPath,
    string? DebugOutputPath
);
