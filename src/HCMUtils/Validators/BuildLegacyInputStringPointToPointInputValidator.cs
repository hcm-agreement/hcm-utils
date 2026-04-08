namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class BuildLegacyInputStringPointToPointInputValidator : BuildLegacyInputStringInputValidator<BuildLegacyInputStringPointToPointInput>
{
    public BuildLegacyInputStringPointToPointInputValidator()
    {
        this.RuleFor(input => input.RxSiteHeight)
            .ExclusiveBetween(-1_000, 10_000)
            .When(input => input.RxSiteHeight != null);
        this.RuleFor(input => input.RxAntennaHeight)
            .ExclusiveBetween(0, 10_000);
        this.RuleFor(input => input.RxServiceAreaRadius)
            .InclusiveBetween(0, 9999);
        this.RuleFor(input => input.RxFrequency)
            .GreaterThanOrEqualTo(1)
            .LessThan(100_000 * Math.Pow(10, 9));
        this.RuleFor(input => input.RxEmissionDesignation)
            .MaximumLength(9);
        this.RuleSet("RxAntennaType", () =>
        {
            this.RuleFor(input => input.RxAntennaType.Horizontal)
                .MaximumLength(7);
            this.RuleFor(input => input.RxAntennaType.Vertical)
                .MaximumLength(7);
        });
        this.RuleFor(input => input.RxAzimuth)
            .ExclusiveBetween(-100.0, 1_000.0);
        this.RuleFor(input => input.RxElevation)
            .ExclusiveBetween(-100.0, 1_000.0);
        this.RuleFor(input => input.RxGain)
            .ExclusiveBetween(-10, 100);
        this.RuleFor(input => input.DepolarizationLoss)
            .ExclusiveBetween(-10, 100);
        this.RuleFor(input => input.FrequencyDifferenceCorrectionFactor)
            .ExclusiveBetween(-1_000, 10_000);
    }
}
