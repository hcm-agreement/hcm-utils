namespace HCMUtilsTests;

using HCMUtils;
using HCMUtils.Types;

public class ITUHelpersTest
{
    [Fact]
    public void ParsesCountriesCorrectly()
    {
        Assert.Equal(Country.Austria, ITUHelpers.ParseCountry("AUT"));
        Assert.Equal(Country.Belgium, ITUHelpers.ParseCountry("BEL"));
        Assert.Equal(Country.CzechRepublic, ITUHelpers.ParseCountry("CZE"));
        Assert.Equal(Country.Denmark, ITUHelpers.ParseCountry("DNK"));
        Assert.Equal(Country.France, ITUHelpers.ParseCountry("F"));
        Assert.Equal(Country.France, ITUHelpers.ParseCountry("F__"));
        Assert.Equal(Country.Germany, ITUHelpers.ParseCountry("D"));
        Assert.Equal(Country.Germany, ITUHelpers.ParseCountry("D__"));
        Assert.Equal(Country.Netherlands, ITUHelpers.ParseCountry("HOL"));
        Assert.Equal(Country.Poland, ITUHelpers.ParseCountry("POL"));
        Assert.Equal(Country.Switzerland, ITUHelpers.ParseCountry("SUI"));
        Assert.Throws<ArgumentException>(() => ITUHelpers.ParseCountry("NONSENSE"));
    }

    [Fact]
    public void ConvertsITULetterCodeStringsCorrectly()
    {
        Assert.Equal("AUT", ITUHelpers.ToITULetterCodeString(Country.Austria));
        Assert.Equal("BEL", ITUHelpers.ToITULetterCodeString(Country.Belgium));
        Assert.Equal("CZE", ITUHelpers.ToITULetterCodeString(Country.CzechRepublic));
        Assert.Equal("DNK", ITUHelpers.ToITULetterCodeString(Country.Denmark));
        Assert.Equal("F", ITUHelpers.ToITULetterCodeString(Country.France));
        Assert.Equal("D", ITUHelpers.ToITULetterCodeString(Country.Germany));
        Assert.Equal("HOL", ITUHelpers.ToITULetterCodeString(Country.Netherlands));
        Assert.Equal("POL", ITUHelpers.ToITULetterCodeString(Country.Poland));
        Assert.Equal("SUI", ITUHelpers.ToITULetterCodeString(Country.Switzerland));
        Assert.Throws<ArgumentException>(() => ITUHelpers.ToITULetterCodeString((Country)(-2)));
    }
}
