namespace HCMUtils.Tests;

using HCMUtils.Types;
using HCMUtils.Validators;
using FluentValidation.TestHelper;

public class BuildLegacyInputStringPointToLineInputValidatorTest
{
    [Fact]
    public void ValidatesInputCorrectly()
    {
        var input = new BuildLegacyInputStringPointToLineInput(
            (8.22, 51.7625),
            10_000,
            ("x000ND00", "x123AB56"),
            1000.0,
            -100.0,
            10_000,
            GainType.Isotropic,
            1000.22,
            400_300_800_000_000,
            true,
            Temperature.Warm,
            10000,
            10000.0,
            "x5M00G7WEF",
            10_000.0,
            Country.Austria,
            Country.Germany,
            10_000,
            "D:\\TOPO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\BORDER\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\MORPHO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "C:\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );

        var validator = new BuildLegacyInputStringPointToLineInputValidator();
        var validationResult = validator.TestValidate(input, options => options.IncludeAllRuleSets());

        validationResult.ShouldHaveValidationErrorFor(input => input.MaxCrossBorderRange)
            .WithErrorMessage("'Max Cross Border Range' must be between -100 and 1000 (exclusive). You entered 10000.");
    }
}
