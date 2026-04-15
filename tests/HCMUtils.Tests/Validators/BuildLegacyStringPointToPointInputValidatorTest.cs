namespace HCMUtils.Tests;

using FluentValidation.TestHelper;
using HCMUtils.Types;
using HCMUtils.Validators;

public class BuildLegacyStringPointToPointInputValidatorTest
{
    [Fact]
    public void ValidatesInputCorrectly()
    {
        var input = new BuildLegacyInputStringPointToPointInput(
            (8.22, 51.7625),
            (18.22, 52.7625),
            10_000,
            -1000,
            ("000ND001", "x123AB56"),
            1000.00,
            -100.00,
            10_000,
            -1_000,
            GainType.Isotropic,
            1_000.22,
            400_300_800_000_000,
            true,
            Temperature.Warm,
            10_000,
            -1,
            10_000,
            -1,
            "13M00G7WEF",
            "15M00G7WEF",
            ("A000ND00", "1000ND00"),
            -112.4,
            1_239.2,
            GainType.Dipole,
            110.0,
            122.0,
            10000,
            12_345,
            Country.Austria,
            Country.Germany,
            "D:\\TOPO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\BORDER\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "D:\\MORPHO\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "C:\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ\\ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );

        var validator = new BuildLegacyStringPointToPointInputValidator();
        var validationResult = validator.TestValidate(input, options => options.IncludeAllRuleSets());

        validationResult.ShouldHaveValidationErrorFor(input => input.RxSiteHeight)
            .WithErrorMessage("'Rx Site Height' must be between -1000 and 10000 (exclusive). You entered -1000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaHeight)
            .WithErrorMessage("'Rx Antenna Height' must be between 0 and 10000 (exclusive). You entered -1000.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxServiceAreaRadius)
            .WithErrorMessage("'Rx Service Area Radius' must be between 0 and 9999. You entered -1.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxFrequency)
            .WithErrorMessage("'Rx Frequency' must be greater than or equal to '1'.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxEmissionDesignation)
            .WithErrorMessage("The length of 'Rx Emission Designation' must be 9 characters or fewer. You entered 10 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaType.Horizontal)
            .WithErrorMessage("The length of 'Rx Antenna Type Item1' must be 7 characters or fewer. You entered 8 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaType.Vertical)
            .WithErrorMessage("The length of 'Rx Antenna Type Item2' must be 7 characters or fewer. You entered 8 characters.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAzimuth)
            .WithErrorMessage("'Rx Azimuth' must be between -100 and 1000 (exclusive). You entered -112.4.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxElevation)
            .WithErrorMessage("'Rx Elevation' must be between -100 and 1000 (exclusive). You entered 1239.2.");
        validationResult.ShouldHaveValidationErrorFor(input => input.RxGain)
            .WithErrorMessage("'Rx Gain' must be between -10 and 100 (exclusive). You entered 110.");
        validationResult.ShouldHaveValidationErrorFor(input => input.DepolarizationLoss)
            .WithErrorMessage("'Depolarization Loss' must be between -10 and 100 (exclusive). You entered 122.");
        validationResult.ShouldHaveValidationErrorFor(input => input.FrequencyDifferenceCorrectionFactor)
            .WithErrorMessage("'Frequency Difference Correction Factor' must be between -1000 and 10000 (exclusive). You entered 12345.");
    }
}
