namespace HCMUtils.Tests;

using FluentValidation.TestHelper;
using HCMUtils.Types;
using HCMUtils.Validators;

public class BuildLegacyStringInputValidatorTest
{
    [Fact]
    public void ValidatesInputCorrectly()
    {
        var input = new BuildLegacyInputStringInput(
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

        var validator = new BuildLegacyStringInputValidator<BuildLegacyInputStringInput>();
        var validationResult = validator.TestValidate(input, options => options.IncludeAllRuleSets());

        validationResult.ShouldHaveValidationErrorFor(input => input.TxSiteHeight)
            .WithErrorMessage("'Tx Site Height' must be between -1000 and 10000 (exclusive). You entered 10000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaType.Horizontal)
            .WithErrorMessage("The length of 'Tx Antenna Type Item1' must be 7 characters or fewer. You entered 8 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaType.Vertical)
            .WithErrorMessage("The length of 'Tx Antenna Type Item2' must be 7 characters or fewer. You entered 8 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAzimuth)
            .WithErrorMessage("'Tx Azimuth' must be between -100 and 1000 (exclusive). You entered 1000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxElevation)
            .WithErrorMessage("'Tx Elevation' must be between -100 and 1000 (exclusive). You entered -100.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxAntennaHeight)
            .WithErrorMessage("'Tx Antenna Height' must be between 0 and 10000 (exclusive). You entered 10000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxPower)
            .WithErrorMessage("'Tx Power' must be between 0 and 1000 (exclusive). You entered 1000.22.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxFrequency)
            .WithErrorMessage("'Tx Frequency' must be less than '100000000000000'.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxServiceAreaRadius)
            .WithErrorMessage("'Tx Service Area Radius' must be between 0 and 9999. You entered 10000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.DistanceOverSea)
            .WithErrorMessage("'Distance Over Sea' must be between 0 and 10000 (exclusive). You entered 10000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TxEmissionDesignation)
            .WithErrorMessage("The length of 'Tx Emission Designation' must be 9 characters or fewer. You entered 10 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.PermissibleFieldStrength)
            .WithErrorMessage("'Permissible Field Strength' must be between -100 and 1000 (exclusive). You entered 10000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.TopoPath)
            .WithErrorMessage("The length of 'Topo Path' must be 63 characters or fewer. You entered 115 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.BorderPath)
            .WithErrorMessage("The length of 'Border Path' must be 63 characters or fewer. You entered 117 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.MorphoPath)
            .WithErrorMessage("The length of 'Morpho Path' must be 63 characters or fewer. You entered 117 characters.");
    }
}
