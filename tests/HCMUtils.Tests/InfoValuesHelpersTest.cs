namespace HCMUtils.Tests;

public class InfoValuesHelpersTests
{
    [Fact]
    public void ConvertsInfoValueArraysCorrectly() => Assert.Equal(
            [
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false,
                true,
                false
            ],
            InfoValuesHelpers.ToInfoValuesArray(
                new Types.InfoValues(
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false
                )
            )
        );
}
