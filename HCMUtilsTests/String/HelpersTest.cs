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
    public void ParsesModesCorrectly()
    {
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("0"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("  0"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("0  "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("10"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode(" 10"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("10 "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("11"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode(" 11"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("11 "));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("12"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode(" 12"));
        Assert.Equal(ModeType.PointToPoint, Helpers.ParseMode("12 "));

        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode("-1"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode(" -1"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode("-9"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode(" -9"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode("-10"));
        Assert.Equal(ModeType.PointToLine, Helpers.ParseMode("-11"));
    }

}