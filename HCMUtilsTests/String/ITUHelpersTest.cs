using HCMUtils.String;
using HCMUtils.Types;

namespace HCMUtilsTests.String;

public class ITUHelpersTest
{
    [Fact]
    public void ParsesITULetterCodesCorrectly()
    {
        Assert.Equal(Country.Austria, ITUHelpers.ParseITULetterCode("AUT"));
        Assert.Equal(Country.Belgium, ITUHelpers.ParseITULetterCode("BEL"));
        Assert.Equal(Country.CzechRepublic, ITUHelpers.ParseITULetterCode("CZE"));
        Assert.Equal(Country.Denmark, ITUHelpers.ParseITULetterCode("DNK"));
        Assert.Equal(Country.France, ITUHelpers.ParseITULetterCode("F"));
        Assert.Equal(Country.France, ITUHelpers.ParseITULetterCode("F__"));
        Assert.Equal(Country.Germany, ITUHelpers.ParseITULetterCode("D"));
        Assert.Equal(Country.Germany, ITUHelpers.ParseITULetterCode("D__"));
        Assert.Equal(Country.Netherlands, ITUHelpers.ParseITULetterCode("HOL"));
        Assert.Equal(Country.Poland, ITUHelpers.ParseITULetterCode("POL"));
        Assert.Equal(Country.Switzerland, ITUHelpers.ParseITULetterCode("SUI"));
        Assert.Throws<Exception>(() => ITUHelpers.ParseITULetterCode("NONSENSE"));
    }
}