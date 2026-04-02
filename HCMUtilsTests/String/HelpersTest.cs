using HCMUtils;
using HCMUtils.String;
using HCMUtils.Types;

namespace HCMUtilsTests.String;

public class HelpersTest
{
    [Fact]
    public void ParsesDMSStringsCorrectly()
    {
        Assert.Equal(51.7025, Helpers.ParseDMSString("51N4209"));
        Assert.Equal(51.7025, Helpers.ParseDMSString("051N4209"));
        Assert.Equal(-51.7025, Helpers.ParseDMSString("51S4209"));
        Assert.Equal(51.7025, Helpers.ParseDMSString("051N4209"));
        Assert.Equal(8.22, Helpers.ParseDMSString("8E1312"));
        Assert.Equal(8.22, Helpers.ParseDMSString("08E1312"));
        Assert.Equal(8.22, Helpers.ParseDMSString("008E1312"));
        Assert.Equal(-8.22, Helpers.ParseDMSString("8W1312"));
        Assert.Equal(-8.22, Helpers.ParseDMSString("08W1312"));
        Assert.Equal(-8.22, Helpers.ParseDMSString("008W1312"));
    }

    [Fact]
    public void ParsesCoordinatesCorrectly()
    {
        Assert.Equal((8.22, 51.7625), Helpers.ParseCoordinates("8E131251N4545"));
        Assert.Equal((8.22, 51.7625), Helpers.ParseCoordinates("08E131251N4545"));
        Assert.Equal((18.22, 51.7625), Helpers.ParseCoordinates("18E131251N4545"));
        Assert.Equal((18.22, 5.7625), Helpers.ParseCoordinates("18E13125N4545"));
        Assert.Equal((18.22, 5.7625), Helpers.ParseCoordinates("18E131205N4545"));
        Assert.Equal((8.22, 5.7625), Helpers.ParseCoordinates("8E131205N4545"));
        Assert.Equal((8.22, 5.7625), Helpers.ParseCoordinates("08E131205N4545"));
    }

    [Fact]
    public void ParsesSINumbersCorrectly()
    {
        Assert.Equal(3_800_000_000, Helpers.ParseSINumber("3.8G"));
        Assert.Equal(145_500_000, Helpers.ParseSINumber("145.500M"));
        Assert.Equal(145_500, Helpers.ParseSINumber("145.500k"));
    }

    [Fact]
    public void ParsesBooleansCorrectly()
    {
        Assert.False(Helpers.ParseBoolean("0"));
        Assert.True(Helpers.ParseBoolean("1"));
        Assert.False(Helpers.ParseBoolean("2"));
        Assert.False(Helpers.ParseBoolean("N"));
        Assert.False(Helpers.ParseBoolean("N "));
        Assert.False(Helpers.ParseBoolean(" N"));
        Assert.False(Helpers.ParseBoolean("n"));
        Assert.False(Helpers.ParseBoolean("n "));
        Assert.False(Helpers.ParseBoolean(" n"));
        Assert.True(Helpers.ParseBoolean("Y"));
        Assert.True(Helpers.ParseBoolean("Y "));
        Assert.True(Helpers.ParseBoolean(" Y"));
        Assert.True(Helpers.ParseBoolean("y"));
        Assert.True(Helpers.ParseBoolean("y "));
        Assert.True(Helpers.ParseBoolean(" y"));
        Assert.True(Helpers.ParseBoolean("1"));
        Assert.False(Helpers.ParseBoolean("2"));
    }

    [Fact]
    public void ParsesTemperaturesCorrectly()
    {
        Assert.Equal(Temperature.Cold, Helpers.ParseTemperature("C"));
        Assert.Equal(Temperature.Warm, Helpers.ParseTemperature("W"));
        Assert.Equal(Temperature.Cold, Helpers.ParseTemperature("c"));
        Assert.Equal(Temperature.Warm, Helpers.ParseTemperature("w"));
        Assert.Throws<Exception>(() => Helpers.ParseTemperature("f"));
    }

    [Fact]
    public void ParsesTxPolarizationCorrectly()
    {
        Assert.Equal(GainType.Dipole, Helpers.ParseGainType("E"));
        Assert.Equal(GainType.Dipole, Helpers.ParseGainType("e"));
        Assert.Equal(GainType.Isotropic, Helpers.ParseGainType("I"));
        Assert.Equal(GainType.Isotropic, Helpers.ParseGainType("i"));
        Assert.Throws<Exception>(() => Helpers.ParseGainType("f"));
    }

    [Fact]
    public void ParsesModeTypesCorrectly()
    {
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("0"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("  0"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("0  "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("10"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType(" 10"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("10 "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("11"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType(" 11"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("11 "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("12"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType(" 12"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseModeType("12 "));

        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType("-1"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType(" -1"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType("-9"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType(" -9"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType("-10"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseModeType("-11"));
    }

    [Fact]
    public void ConvertsDMSStringsCorrectly()
    {
        Assert.Equal("51N4209", Helpers.ToDMSString(51.7025, true));
        Assert.Equal("51S4209", Helpers.ToDMSString(-51.7025, true));
        Assert.Equal("008E1312", Helpers.ToDMSString(8.22, false));
        Assert.Equal("008W1312", Helpers.ToDMSString(-8.22, false));
    }

    [Fact]
    public void ConvertsCoordinateStringsCorrectly()
    {
        Assert.Equal("008E131251N4545", Helpers.ToCoordinatesString((8.22, 51.7625)));
        Assert.Equal("008W131251S4545", Helpers.ToCoordinatesString((-8.22, -51.7625)));
        Assert.Equal("018E131251N4545", Helpers.ToCoordinatesString((18.22, 51.7625)));
        Assert.Equal("018W131251S4545", Helpers.ToCoordinatesString((-18.22, -51.7625)));
        Assert.Equal("008E131201N4545", Helpers.ToCoordinatesString((8.22, 1.7625)));
        Assert.Equal("008W131201S4545", Helpers.ToCoordinatesString((-8.22, -1.7625)));
        Assert.Equal("018E131201N4545", Helpers.ToCoordinatesString((18.22, 1.7625)));
        Assert.Equal("018W131201S4545", Helpers.ToCoordinatesString((-18.22, -1.7625)));
    }

    [Fact]
    public void ConvertsGainTypeStringsCorrectly()
    {
        Assert.Equal('E', Helpers.ToGainTypeString(GainType.Dipole));
        Assert.Equal('I', Helpers.ToGainTypeString(GainType.Isotropic));
    }

    [Fact]
    public void ConvertsFrequencyStringsCorrectly()
    {
        Assert.Equal("10.00000k", Helpers.ToFrequencyString(10_000.0, SIPrefix.k));
        Assert.Equal("10.12345M", Helpers.ToFrequencyString(10_123_450.0, SIPrefix.M));
        Assert.Equal("3.80000G", Helpers.ToFrequencyString(3_800_000_000.0, SIPrefix.G));
    }

    [Fact]
    public void ConvertsBooleanStringsCorrectly()
    {
        Assert.Equal("1", Helpers.ToBooleanString(true));
        Assert.Equal("0", Helpers.ToBooleanString(false));
    }

    [Fact]
    public void ConvertsTemperatureStringsCorrectly()
    {
        Assert.Equal("W", Helpers.ToTemperatureString(Temperature.Warm));
        Assert.Equal("C", Helpers.ToTemperatureString(Temperature.Cold));
    }

    [Fact]
    public void BuildsLegacyStringsCorrectly()
    {
        // point to line
        Assert.Equal(
            "008E131251N4545                 24    000ND00123AB56 23.5-11.0   2    I 26.22 3800.00000M1W   20      10.2                     5M00G7WEF                                   2.1    AUTD__  2D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              C:\\",
            Helpers.BuildLegacyInputString(
                (8.22, 51.7625),
                24,
                ("000ND00", "123AB56"),
                23.45,
                -10.98,
                2,
                GainType.Isotropic,
                26.22,
                3_800_000_000,
                true,
                Temperature.Warm,
                20,
                10.2,
                "5M00G7WEF",
                2.1,
                Country.Austria,
                Country.Germany,
                2,
                "D:\\TOPO",
                "D:\\BORDER",
                "D:\\MORPHO",
                "C:\\"
            )
        );

        // point to point
        Assert.Equal(
            "008E131251N4545018E131252N4545  24  22000ND00123AB56 23.5-11.0   2   4I 26.22 3800.00000M1W   20   18 10.2 8800.00000M3M00G7WEF5M00G7WEF000ND00000ND00 12.4 -9.2E10.0 2.0  2.1   1AUTD__   D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              ",
            Helpers.BuildLegacyInputString(
                (8.22, 51.7625),
                (18.22, 52.7625),
                24,
                22,
                ("000ND00", "123AB56"),
                23.45,
                -10.98,
                2,
                4,
                GainType.Isotropic,
                26.22,
                3_800_000_000,
                true,
                Temperature.Warm,
                20,
                18,
                10.2,
                8_800_000_000,
                "3M00G7WEF",
                "5M00G7WEF",
                ("000ND00", "000ND00"),
                12.4,
                -9.2,
                GainType.Dipole,
                10.0,
                2.0,
                2.1,
                1,
                Country.Austria,
                Country.Germany,
                "D:\\TOPO",
                "D:\\BORDER",
                "D:\\MORPHO",
                null
            )
        );

        // test null parameters
        // point to line
        Assert.Equal(
            "008E131251N4545                       000ND00123AB56 23.5-11.0   2    I 26.22 3800.00000M1    20                               5M00G7WEF                                          AUTD__  2D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              C:\\",
            Helpers.BuildLegacyInputString(
                (8.22, 51.7625),
                null,
                ("000ND00", "123AB56"),
                23.45,
                -10.98,
                2,
                GainType.Isotropic,
                26.22,
                3_800_000_000,
                true,
                null,
                20,
                null,
                "5M00G7WEF",
                null,
                Country.Austria,
                Country.Germany,
                2,
                "D:\\TOPO",
                "D:\\BORDER",
                "D:\\MORPHO",
                "C:\\"
            )
        );

        // point to point
        Assert.Equal(
            "008E131251N4545018E131252N4545        000ND00123AB56 23.5-11.0   2   4I 26.22 3800.00000M1    20   18      8800.00000M3M00G7WEF5M00G7WEF000ND00000ND00 12.4 -9.2E10.0 2.0  2.1    AUTD__   D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              ",
            Helpers.BuildLegacyInputString(
                (8.22, 51.7625),
                (18.22, 52.7625),
                null,
                null,
                ("000ND00", "123AB56"),
                23.45,
                -10.98,
                2,
                4,
                GainType.Isotropic,
                26.22,
                3_800_000_000,
                true,
                null,
                20,
                18,
                null,
                8_800_000_000,
                "3M00G7WEF",
                "5M00G7WEF",
                ("000ND00", "000ND00"),
                12.4,
                -9.2,
                GainType.Dipole,
                10.0,
                2.0,
                2.1,
                null,
                Country.Austria,
                Country.Germany,
                "D:\\TOPO",
                "D:\\BORDER",
                "D:\\MORPHO",
                null
            )
        ); 
    }
}