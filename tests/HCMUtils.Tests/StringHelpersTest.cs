namespace HCMUtils.Tests;

using FluentValidation;
using HCMUtils;
using HCMUtils.Types;

public class StringHelpersTest
{
    [Fact]
    public void ParsesDMSStringsCorrectly()
    {
        Assert.Equal(51.7025, StringHelpers.ParseDMSString("51N4209"));
        Assert.Equal(51.7025, StringHelpers.ParseDMSString("051N4209"));
        Assert.Equal(-51.7025, StringHelpers.ParseDMSString("51S4209"));
        Assert.Equal(51.7025, StringHelpers.ParseDMSString("051N4209"));
        Assert.Equal(8.22, StringHelpers.ParseDMSString("8E1312"));
        Assert.Equal(8.22, StringHelpers.ParseDMSString("08E1312"));
        Assert.Equal(8.22, StringHelpers.ParseDMSString("008E1312"));
        Assert.Equal(-8.22, StringHelpers.ParseDMSString("8W1312"));
        Assert.Equal(-8.22, StringHelpers.ParseDMSString("08W1312"));
        Assert.Equal(-8.22, StringHelpers.ParseDMSString("008W1312"));
    }

    [Fact]
    public void ParsesCoordinatesCorrectly()
    {
        Assert.Equal((8.22, 51.7625), StringHelpers.ParseCoordinates("8E131251N4545"));
        Assert.Equal((8.22, 51.7625), StringHelpers.ParseCoordinates("08E131251N4545"));
        Assert.Equal((18.22, 51.7625), StringHelpers.ParseCoordinates("18E131251N4545"));
        Assert.Equal((18.22, 5.7625), StringHelpers.ParseCoordinates("18E13125N4545"));
        Assert.Equal((18.22, 5.7625), StringHelpers.ParseCoordinates("18E131205N4545"));
        Assert.Equal((8.22, 5.7625), StringHelpers.ParseCoordinates("8E131205N4545"));
        Assert.Equal((8.22, 5.7625), StringHelpers.ParseCoordinates("08E131205N4545"));
    }

    [Fact]
    public void ParsesSINumbersCorrectly()
    {
        Assert.Equal(3_800_000_000, StringHelpers.ParseSINumber("3.8G"));
        Assert.Equal(145_500_000, StringHelpers.ParseSINumber("145.500M"));
        Assert.Equal(145_500, StringHelpers.ParseSINumber("145.500k"));
        Assert.Throws<ArgumentException>(() => StringHelpers.ParseSINumber("NO"));
    }

    [Fact]
    public void ParsesSIPrefixesCorrectly()
    {
        Assert.Equal(SIPrefix.G, StringHelpers.ParseSIPrefix("G"));
        Assert.Equal(SIPrefix.M, StringHelpers.ParseSIPrefix("M"));
        Assert.Equal(SIPrefix.k, StringHelpers.ParseSIPrefix("k"));
        Assert.Throws<ArgumentException>(() => StringHelpers.ParseSIPrefix("NO"));
    }

    [Fact]
    public void ReturnsSIMultipliersCorrectly()
    {
        Assert.Equal(1_000_000_000, StringHelpers.GetSIMultiplier(SIPrefix.G));
        Assert.Equal(1_000_000, StringHelpers.GetSIMultiplier(SIPrefix.M));
        Assert.Equal(1_000, StringHelpers.GetSIMultiplier(SIPrefix.k));
        Assert.Throws<ArgumentException>(() => StringHelpers.GetSIMultiplier((SIPrefix)1337));
    }

    [Fact]
    public void ParsesBooleansCorrectly()
    {
        Assert.False(StringHelpers.ParseBoolean("0"));
        Assert.True(StringHelpers.ParseBoolean("1"));
        Assert.False(StringHelpers.ParseBoolean("2"));
        Assert.False(StringHelpers.ParseBoolean("N"));
        Assert.False(StringHelpers.ParseBoolean("N "));
        Assert.False(StringHelpers.ParseBoolean(" N"));
        Assert.False(StringHelpers.ParseBoolean("n"));
        Assert.False(StringHelpers.ParseBoolean("n "));
        Assert.False(StringHelpers.ParseBoolean(" n"));
        Assert.True(StringHelpers.ParseBoolean("Y"));
        Assert.True(StringHelpers.ParseBoolean("Y "));
        Assert.True(StringHelpers.ParseBoolean(" Y"));
        Assert.True(StringHelpers.ParseBoolean("y"));
        Assert.True(StringHelpers.ParseBoolean("y "));
        Assert.True(StringHelpers.ParseBoolean(" y"));
        Assert.True(StringHelpers.ParseBoolean("1"));
        Assert.False(StringHelpers.ParseBoolean("2"));
    }

    [Fact]
    public void ParsesTemperaturesCorrectly()
    {
        Assert.Equal(Temperature.Cold, StringHelpers.ParseTemperature("C"));
        Assert.Equal(Temperature.Warm, StringHelpers.ParseTemperature("W"));
        Assert.Equal(Temperature.Cold, StringHelpers.ParseTemperature("c"));
        Assert.Equal(Temperature.Warm, StringHelpers.ParseTemperature("w"));
        Assert.Throws<ArgumentException>(() => StringHelpers.ParseTemperature("f"));
    }

    [Fact]
    public void ParsesTxPolarizationCorrectly()
    {
        Assert.Equal(GainType.Dipole, StringHelpers.ParseGainType("E"));
        Assert.Equal(GainType.Dipole, StringHelpers.ParseGainType("e"));
        Assert.Equal(GainType.Isotropic, StringHelpers.ParseGainType("I"));
        Assert.Equal(GainType.Isotropic, StringHelpers.ParseGainType("i"));
        Assert.Throws<ArgumentException>(() => StringHelpers.ParseGainType("f"));
    }

    [Fact]
    public void ParsesModeTypesCorrectly()
    {
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("0"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("  0"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("0  "));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("10"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType(" 10"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("10 "));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("11"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType(" 11"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("11 "));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("12"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType(" 12"));
        Assert.Equal(ModeType.PointToPoint, StringHelpers.ParseModeType("12 "));

        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType("-1"));
        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType(" -1"));
        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType("-9"));
        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType(" -9"));
        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType("-10"));
        Assert.Equal(ModeType.PointToLine, StringHelpers.ParseModeType("-11"));
    }

    [Fact]
    public void ConvertsDMSStringsCorrectly()
    {
        Assert.Equal("51N4209", StringHelpers.ToDMSString(51.7025, true));
        Assert.Equal("51S4209", StringHelpers.ToDMSString(-51.7025, true));
        Assert.Equal("008E1312", StringHelpers.ToDMSString(8.22, false));
        Assert.Equal("008W1312", StringHelpers.ToDMSString(-8.22, false));
    }

    [Fact]
    public void ConvertsCoordinateStringsCorrectly()
    {
        Assert.Equal("008E131251N4545", StringHelpers.ToCoordinatesString((8.22, 51.7625)));
        Assert.Equal("008W131251S4545", StringHelpers.ToCoordinatesString((-8.22, -51.7625)));
        Assert.Equal("018E131251N4545", StringHelpers.ToCoordinatesString((18.22, 51.7625)));
        Assert.Equal("018W131251S4545", StringHelpers.ToCoordinatesString((-18.22, -51.7625)));
        Assert.Equal("008E131201N4545", StringHelpers.ToCoordinatesString((8.22, 1.7625)));
        Assert.Equal("008W131201S4545", StringHelpers.ToCoordinatesString((-8.22, -1.7625)));
        Assert.Equal("018E131201N4545", StringHelpers.ToCoordinatesString((18.22, 1.7625)));
        Assert.Equal("018W131201S4545", StringHelpers.ToCoordinatesString((-18.22, -1.7625)));
    }

    [Fact]
    public void ConvertsGainTypeStringsCorrectly()
    {
        Assert.Equal('E', StringHelpers.ToGainTypeString(GainType.Dipole));
        Assert.Equal('I', StringHelpers.ToGainTypeString(GainType.Isotropic));
    }

    [Fact]
    public void ConvertsFrequencyStringsCorrectly()
    {
        Assert.Equal("10.00000k", StringHelpers.ToFrequencyString(10_000.0, SIPrefix.k));
        Assert.Equal("10.12345M", StringHelpers.ToFrequencyString(10_123_450.0, SIPrefix.M));
        Assert.Equal("3.80000G", StringHelpers.ToFrequencyString(3_800_000_000.0, SIPrefix.G));
    }

    [Fact]
    public void ConvertsSIPrefixesCorrectly()
    {
        Assert.Equal("G", StringHelpers.ToSIPrefixString(SIPrefix.G));
        Assert.Equal("M", StringHelpers.ToSIPrefixString(SIPrefix.M));
        Assert.Equal("k", StringHelpers.ToSIPrefixString(SIPrefix.k));
        Assert.Throws<ArgumentException>(() => StringHelpers.ToSIPrefixString((SIPrefix)1337));
    }


    [Fact]
    public void ConvertsBooleanStringsCorrectly()
    {
        Assert.Equal("1", StringHelpers.ToBooleanString(true));
        Assert.Equal("0", StringHelpers.ToBooleanString(false));
    }

    [Fact]
    public void ConvertsTemperatureStringsCorrectly()
    {
        Assert.Equal("W", StringHelpers.ToTemperatureString(Temperature.Warm));
        Assert.Equal("C", StringHelpers.ToTemperatureString(Temperature.Cold));
    }

    [Fact]
    public void ValidatesLegacyInputStringInputsCorrectly()
    {
        Assert.Throws<ValidationException>(() => StringHelpers.BuildLegacyInputString(
            new BuildLegacyInputStringPointToPointInput(
                (8.22, 51.7625),
                (18.22, 52.7625),
                10_000, // too much
                -1000, // too little
                ("000ND001", "x123AB56"), // both too long
                123.45, // too much
                -100.98, // too little
                10_000, // too much
                -1_000, // too little
                GainType.Isotropic,
                1126.22, // too much
                400_300_800_000_000, // too much
                true,
                Temperature.Warm,
                112_320, // too much
                -18_123, // too little
                1210.2, // too much
                -1, // too little
                "13M00G7WEF", // too long
                "15M00G7WEF", // too long
                ("A000ND00", "1000ND00"), // both too long
                -112.4, // too little
                1_239.2, // too much
                GainType.Dipole,
                110.0, // too much
                122.0, // too much
                -2.1, // too little
                12_345, // too much
                Country.Austria,
                Country.Germany,
                "D:\\TOPO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "D:\\BORDER\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "D:\\MORPHO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "C:\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            )
        ));
    }

    [Fact]
    public void BuildsLegacyStringsCorrectly()
    {
        // point to line
        Assert.Equal(
            "008E131251N4545                 24    000ND00123AB56 23.5-11.0   2    I 26.22 3800.00000M1W   20      10.2                     5M00G7WEF                                   2.1    AUTD__  2D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              C:\\",
            StringHelpers.BuildLegacyInputString(
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
            "008E131251N4545018E131252N4545  24  22000ND00123AB56 23.5-11.0   2   4I 26.22 3800.00000M1W   20   18 10.2 8800.00000M3M00G7WEF5M00G7WEF000ND00000ND00 12.4 -9.2E10.0 2.0  2.1   1AUTD__   D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              C:\\",
            StringHelpers.BuildLegacyInputString(
                new BuildLegacyInputStringPointToPointInput(
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
                    "C:\\"
                )
            )
        );

        // test null parameters
        // point to line
        Assert.Equal(
            "008E131251N4545                       000ND00123AB56 23.5-11.0   2    I 26.22 3800.00000M1    20                               5M00G7WEF                                          AUTD__  2D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              ",
            StringHelpers.BuildLegacyInputString(
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
                null
            )
        );

        // point to point
        Assert.Equal(
            "008E131251N4545018E131252N4545        000ND00123AB56 23.5-11.0   2   4I 26.22 3800.00000M1    20   18      8800.00000M3M00G7WEF5M00G7WEF000ND00000ND00 12.4 -9.2E10.0 2.0         AUTD__   D:\\TOPO                                                        D:\\BORDER                                                      D:\\MORPHO                                                                                                              ",
            StringHelpers.BuildLegacyInputString(
                new BuildLegacyInputStringPointToPointInput(
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
                    null,
                    null,
                    Country.Austria,
                    Country.Germany,
                    "D:\\TOPO",
                    "D:\\BORDER",
                    "D:\\MORPHO",
                    null
                )
            )
        );
    }
}
