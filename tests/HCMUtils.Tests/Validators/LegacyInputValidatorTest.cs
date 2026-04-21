namespace HCMUtils.Tests;

using FluentValidation.TestHelper;
using HCMUtils.Types;
using HCMUtils.Validators;

public class LegacyInputValidatorTest
{
    [Fact]
    public void ValidatesInputCorrectly()
    {
        var input = new LegacyInput(
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
            Country.Germany,
            "D:\\TOPO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\BORDER\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\MORPHO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "C:\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );

        var validator = new LegacyInputValidator<LegacyInput>();
        var validationResult = validator.TestValidate(input, options => options.IncludeAllRuleSets());

        validationResult.ShouldHaveValidationErrorFor(input => input.TxSiteHeight);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaType.Horizontal);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaType.Vertical);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAzimuth);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxElevation);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaHeight);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxPower);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxFrequency);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxServiceAreaRadius);
        validationResult.ShouldHaveValidationErrorFor(input => input.DistanceOverSea);
        validationResult.ShouldHaveValidationErrorFor(input => input.TxEmissionDesignation);
        validationResult.ShouldHaveValidationErrorFor(input => input.PermissibleFieldStrength);
        validationResult.ShouldHaveValidationErrorFor(input => input.TopoPath);
        validationResult.ShouldHaveValidationErrorFor(input => input.BorderPath);
        validationResult.ShouldHaveValidationErrorFor(input => input.MorphoPath);
    }
}
