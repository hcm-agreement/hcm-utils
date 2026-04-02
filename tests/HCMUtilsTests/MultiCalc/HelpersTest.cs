using HCMUtils.MultiCalc;
using HCMUtils.Types;

[assembly: CaptureConsole]

namespace HCMUtils.Tests.MultiCalc;

public class HelpersTest
{
    [Fact]
    public void ParsesModeCorrectly()
    {
        Assert.IsType<PointToLineInput>(Helpers.ParseMultiCalc(BuildPointToLineTestInput(mode: "-1")).First());
        Assert.Equal(-1, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(mode: "-1")).Mode);
        Assert.IsType<PointToLineInput>(Helpers.ParseMultiCalc(BuildPointToLineTestInput(mode: "-9")).First());
        Assert.Equal(-9, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(mode: "-9")).Mode);
        Assert.IsType<PointToLineInput>(Helpers.ParseMultiCalc(BuildPointToLineTestInput(mode: "-10")).First());
        Assert.Equal(-10, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(mode: "-10")).Mode);
        Assert.IsType<PointToLineInput>(Helpers.ParseMultiCalc(BuildPointToLineTestInput(mode: "-11")).First());
        Assert.Equal(-11, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(mode: "-11")).Mode);

        Assert.IsType<PointToPointInput>(Helpers.ParseMultiCalc(BuildPointToPointTestInput(mode: "0")).First());
        Assert.Equal(0, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(mode: "0")).Mode);
        Assert.IsType<PointToPointInput>(Helpers.ParseMultiCalc(BuildPointToPointTestInput(mode: "10")).First());
        Assert.Equal(10, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(mode: "10")).Mode);
        Assert.IsType<PointToPointInput>(Helpers.ParseMultiCalc(BuildPointToPointTestInput(mode: "11")).First());
        Assert.Equal(11, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(mode: "11")).Mode);
        Assert.IsType<PointToPointInput>(Helpers.ParseMultiCalc(BuildPointToPointTestInput(mode: "12")).First());
        Assert.Equal(12, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(mode: "12")).Mode);
    }

    [Fact]
    public void ParsesTxCoordinatesCorrectly()
    {
        Assert.Equal((8.7225, 51.7025), Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txCoordinates: "8E432151N4209")).TxCoordinates);

        Assert.Equal((8.7225, 51.7025), Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txCoordinates: "8E432151N4209")).TxCoordinates);
    }

    [Fact]
    public void ParsesRxCoordinatesCorrectly() => Assert.Equal((8.7225, 51.7025), Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxCoordinates: "8E432151N4209")).RxCoordinates);

    [Fact]
    public void ParsesTxSiteHeightCorrectly()
    {
        Assert.Equal(10, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txSiteHeight: "10")).TxSiteHeight);
        Assert.Null(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txSiteHeight: "")).TxSiteHeight);

        Assert.Equal(10, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txSiteHeight: "10")).TxSiteHeight);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txSiteHeight: "")).TxSiteHeight);
    }

    [Fact]
    public void ParsesRxHeightAboveSeaLevelCorrectly()
    {
        Assert.Equal(10, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxHeightAboveSeaLevel: "10")).RxSiteHeight);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxHeightAboveSeaLevel: "")).RxSiteHeight);
    }

    [Fact]
    public void SetsTxAntennaTypeCorrectly()
    {
        Assert.Equal(("000ND00", "1234AB56"), Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txHorizontalAntennaType: "000ND00", txVerticalAntennaType: "1234AB56")).TxAntennaType);

        Assert.Equal(("000ND00", "1234AB56"), Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txHorizontalAntennaType: "000ND00", txVerticalAntennaType: "1234AB56")).TxAntennaType);
    }

    [Fact]
    public void SetsRxAntennaTypeCorrectly() => Assert.Equal(("000ND00", "1234AB56"), Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxHorizontalAntennaType: "000ND00", rxVerticalAntennaType: "1234AB56")).RxAntennaType);

    [Fact]
    public void SetsTxEmissionDesignationCorrectly() => Assert.Equal("5M00G7WEF", Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txEmissionDesignation: "5M00G7WEF")).TxEmissionDesignation);

    [Fact]
    public void SetsRxEmissionDesignationCorrectly() => Assert.Equal("5M00G7WEF", Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxEmissionDesignation: "5M00G7WEF")).RxEmissionDesignation);

    [Fact]
    public void ParsesTxAzimuthCorrectly()
    {
        Assert.Equal(20.5, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txAzimuth: " 20.5")).TxAzimuth);

        Assert.Equal(20.5, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txAzimuth: " 20.5")).TxAzimuth);
    }

    [Fact]
    public void ParsesTxElevationCorrectly()
    {
        Assert.Equal(67.12, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txElevation: "67.12")).TxElevation);

        Assert.Equal(67.12, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txElevation: "67.12")).TxElevation);
    }

    [Fact]
    public void ParsesRxAzimuthCorrectly() => Assert.Equal(20.5, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxAzimuth: " 20.5")).RxAzimuth);

    [Fact]
    public void ParsesRxElevationCorrectly() => Assert.Equal(67.12, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxElevation: "67.12")).RxElevation);
    [Fact]
    public void ParsesTxAntennaHeightCorrectly()
    {
        Assert.Equal(2, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txAntennaHeight: " 2")).TxAntennaHeight);

        Assert.Equal(2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txAntennaHeight: " 2")).TxAntennaHeight);
    }

    [Fact]
    public void ParsesRxAntennaHeightCorrectly() => Assert.Equal(2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxAntennaHeight: " 2")).RxAntennaHeight);

    [Fact]
    public void ParsesTxPowerCorrectly()
    {
        Assert.Equal(25.2, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txPower: " 25.2")).TxPower);

        Assert.Equal(25.2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txPower: " 25.2")).TxPower);
    }

    [Fact]
    public void ParsesTxGainTypeCorrectly()
    {
        Assert.Equal(GainType.Dipole, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txGainType: "E")).TxGainType);
        Assert.Equal(GainType.Isotropic, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txGainType: "I")).TxGainType);

        Assert.Equal(GainType.Dipole, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txGainType: "E")).TxGainType);
        Assert.Equal(GainType.Isotropic, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txGainType: "I")).TxGainType);
    }

    [Fact]
    public void ParsesRxGainTypeCorrectly()
    {
        Assert.Equal(GainType.Dipole, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxGainType: "E")).RxGainType);
        Assert.Equal(GainType.Isotropic, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxGainType: "I")).RxGainType);
    }

    [Fact]
    public void ParsesTxFrequencyCorrectly()
    {
        Assert.Equal(145_500_000, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txFrequency: "145.500M")).TxFrequency);
        Assert.Equal(3_800_000_000, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txFrequency: "3.8G")).TxFrequency);
        Assert.Equal(145_500_000, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txFrequency: "145500.0k")).TxFrequency);

        Assert.Equal(145_500_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txFrequency: "145.500M")).TxFrequency);
        Assert.Equal(3_800_000_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txFrequency: "3.8G")).TxFrequency);
        Assert.Equal(145_500_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txFrequency: "145500.0k")).TxFrequency);
    }

    [Fact]
    public void ParsesRxFrequencyCorrectly()
    {
        Assert.Equal(145_500_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxFrequency: "145.500M")).RxFrequency);
        Assert.Equal(3_800_000_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxFrequency: "3.8G")).RxFrequency);
        Assert.Equal(145_500_000, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxFrequency: "145500.0k")).RxFrequency);
    }

    [Fact]
    public void ParsesChannelOccupationCorrectly()
    {
        Assert.True(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(channelOccupation: "1")).ChannelOccupation);
        Assert.False(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(channelOccupation: "0")).ChannelOccupation);

        Assert.True(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(channelOccupation: "1")).ChannelOccupation);
        Assert.False(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(channelOccupation: "0")).ChannelOccupation);
    }

    [Fact]
    public void ParsesSeaTemperatureCorrectly()
    {
        Assert.Equal(Temperature.Warm, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(seaTemperature: "W")).SeaTemperature);
        Assert.Equal(Temperature.Cold, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(seaTemperature: "C")).SeaTemperature);
        Assert.Null(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(seaTemperature: "")).SeaTemperature);

        Assert.Equal(Temperature.Warm, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(seaTemperature: "W")).SeaTemperature);
        Assert.Equal(Temperature.Cold, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(seaTemperature: "C")).SeaTemperature);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(seaTemperature: "")).SeaTemperature);
    }

    [Fact]
    public void ParsesDistanceOverSeaCorrectly()
    {
        Assert.Equal(10.2, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(distanceOverSea: "10.2")).DistanceOverSea);
        Assert.Null(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(distanceOverSea: "")).DistanceOverSea);

        Assert.Equal(10.2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(distanceOverSea: "10.2")).DistanceOverSea);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(distanceOverSea: "")).DistanceOverSea);
    }

    [Fact]
    public void ParsesTxServiceAreaRadiusCorrectly()
    {
        Assert.Equal(10, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txServiceAreaRadius: "10")).TxServiceAreaRadius);

        Assert.Equal(10, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txServiceAreaRadius: "10")).TxServiceAreaRadius);
    }

    [Fact]
    public void ParsesRxServiceAreaRadiusCorrectly() => Assert.Equal(1, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxServiceAreaRadius: "1")).RxServiceAreaRadius);

    [Fact]
    public void ParsesDistanceToBorderlineCorrectly() => Assert.Equal(40, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(distanceToBorderline: "40")).DistanceToBorderline);

    [Fact]
    public void ParsesTxCountryCorrectly()
    {
        Assert.Equal(Country.Austria, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txCountry: "AUT")).TxCountry);
        Assert.Equal(Country.Netherlands, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(txCountry: "HOL")).TxCountry);
    }

    [Fact]
    public void ParsesTargetCountryCorrectly() => Assert.Equal(Country.Germany, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(txCountry: "D__")).TargetCountry);

    [Fact]
    public void ParsesPermissibleFieldStrengthCorrectly()
    {
        Assert.Equal(25.2, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(permissibleFieldStrength: "25.2")).PermissibleFieldStrength);
        Assert.Null(Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(permissibleFieldStrength: "")).PermissibleFieldStrength);

        Assert.Equal(25.2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(permissibleFieldStrength: "25.2")).PermissibleFieldStrength);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(permissibleFieldStrength: "")).PermissibleFieldStrength);
    }

    [Fact]
    public void ParsesMaxCrossBorderRangeCorrectly() => Assert.Equal(5, Helpers.ParsePointToLineMultiCalc(BuildPointToLineTestInput(maxCrossBorderRange: "5")).MaxCrossBorderRange);

    [Fact]
    public void ParsesRxGainCorrectly() => Assert.Equal(8.2, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(rxGain: "8.2")).RxGain);

    [Fact]
    public void ParsesDepolarizationLossCorrectly() => Assert.Equal(0.5, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(depolarizationLoss: "0.5")).DepolarizationLoss);

    [Fact]
    public void ParsesFrequencyDifferenceCorrectionFactorCorrectly()
    {
        Assert.Equal(1, Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(frequencyDifferenceCorrectionFactor: "1")).FrequencyDifferenceCorrectionFactor);
        Assert.Null(Helpers.ParsePointToPointMultiCalc(BuildPointToPointTestInput(frequencyDifferenceCorrectionFactor: "")).FrequencyDifferenceCorrectionFactor);
    }

    private static List<string> BuildPointToLineTestInput(
        string mode = "1",
        string txCoordinates = "8E422351N1337",
        string txSiteHeight = "",
        string txHorizontalAntennaType = "000ND00",
        string txVerticalAntennaType = "1234AB56",
        string txAzimuth = "0.0",
        string txElevation = "0.0",
        string txAntennaHeight = "0",
        string txPower = "0.0",
        string txGainType = "E",
        string txFrequency = "0.0M",
        string channelOccupation = "0",
        string seaTemperature = "W",
        string distanceOverSea = "",
        string txServiceAreaRadius = "0",
        string distanceToBorderline = "0",
        string txCountry = "D",
        string targetCountry = "D",
        string permissibleFieldStrength = "",
        string maxCrossBorderRange = "0",
        string txEmissionDesignation = "5M00G7WEF"
    ) => [
            mode,
            txCoordinates,
            txSiteHeight,
            txHorizontalAntennaType,
            txVerticalAntennaType,
            txAzimuth,
            txElevation,
            txAntennaHeight,
            txPower,
            txGainType,
            txFrequency,
            channelOccupation,
            seaTemperature,
            distanceOverSea,
            txServiceAreaRadius,
            distanceToBorderline,
            txCountry,
            targetCountry,
            permissibleFieldStrength,
            maxCrossBorderRange,
            txEmissionDesignation
        ];

    private static List<string> BuildPointToPointTestInput(
        string mode = "0",
        string txCoordinates = "8E422351N1337",
        string txSiteHeight = "",
        string txHorizontalAntennaType = "000ND00",
        string txVerticalAntennaType = "1234AB56",
        string txAzimuth = "0.0",
        string txElevation = "0.0",
        string txAntennaHeight = "0",
        string txPower = "0.0",
        string txGainType = "E",
        string txFrequency = "0.0M",
        string channelOccupation = "0",
        string seaTemperature = "W",
        string distanceOverSea = "",
        string txServiceAreaRadius = "0",
        string rxCoordinates = "8E422351N1337",
        string rxHeightAboveSeaLevel = "",
        string rxAntennaHeight = "2",
        string txCountry = "D__",
        string rxCountry = "AUT",
        string permissibleFieldStrength = "",
        string rxFrequency = "0.0M",
        string txEmissionDesignation = "5M00G7WEF",
        string rxEmissionDesignation = "5M00G7WEF",
        string rxHorizontalAntennaType = "000ND00",
        string rxVerticalAntennaType = "1234AB56",
        string rxAzimuth = "0.0",
        string rxElevation = "0.0",
        string rxGainType = "E",
        string rxGain = "0.0",
        string depolarizationLoss = "0.0",
        string frequencyDifferenceCorrectionFactor = "",
        string rxServiceAreaRadius = "0"
    ) => [
            mode,
            txCoordinates,
            txSiteHeight,
            txHorizontalAntennaType,
            txVerticalAntennaType,
            txAzimuth,
            txElevation,
            txAntennaHeight,
            txPower,
            txGainType,
            txFrequency,
            channelOccupation,
            seaTemperature,
            distanceOverSea,
            txServiceAreaRadius,
            rxCoordinates,
            rxHeightAboveSeaLevel,
            rxAntennaHeight,
            txCountry,
            rxCountry,
            permissibleFieldStrength,
            rxFrequency,
            txEmissionDesignation,
            rxEmissionDesignation,
            rxHorizontalAntennaType,
            rxVerticalAntennaType,
            rxAzimuth,
            rxElevation,
            rxGainType,
            rxGain,
            depolarizationLoss,
            frequencyDifferenceCorrectionFactor,
            rxServiceAreaRadius
        ];

    [Fact]
    public void ParsesMultiCalcLinesCorrectly() => Assert.NotEmpty(Helpers.ParseMultiCalc(File.ReadLines("fixtures/MultiCalc.txt")));
}
