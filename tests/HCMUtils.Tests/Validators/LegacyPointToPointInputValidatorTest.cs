namespace HCMUtils.Tests;

using FluentValidation.TestHelper;
using HCMUtils.Types;
using HCMUtils.Validators;

public class LegacyPointToPointInputValidatorTest
{
    [Fact]
    public void ValidatesInputCorrectly()
    {
        var input = new LegacyPointToPointInput(
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

        var validator = new LegacyPointToPointInputValidator();
        var validationResult = validator.TestValidate(input, options => options.IncludeAllRuleSets());

        validationResult.ShouldHaveValidationErrorFor(input => input.RxSiteHeight);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaHeight);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxServiceAreaRadius);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxFrequency);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxEmissionDesignation);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaType.Horizontal);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAntennaType.Vertical);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxAzimuth);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxElevation);
        validationResult.ShouldHaveValidationErrorFor(input => input.RxGain);
        validationResult.ShouldHaveValidationErrorFor(input => input.DepolarizationLoss);
        validationResult.ShouldHaveValidationErrorFor(input => input.FrequencyDifferenceCorrectionFactor);
    }
}
