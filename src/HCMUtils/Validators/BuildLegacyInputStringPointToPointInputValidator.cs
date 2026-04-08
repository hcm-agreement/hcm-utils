namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class BuildLegacyInputStringPointToPointInputValidator : AbstractValidator<BuildLegacyInputStringPointToPointInput>
{
    public BuildLegacyInputStringPointToPointInputValidator()
    {
        this.RuleFor(input => input.TxSiteHeight)
            .ExclusiveBetween(-1_000, 10_000)
            .When(input => input.TxSiteHeight != null);
        this.RuleFor(input => input.RxSiteHeight)
            .ExclusiveBetween(-1_000, 10_000)
            .When(input => input.RxSiteHeight != null);
        this.RuleSet("TxAntennaType", () =>
        {
            this.RuleFor(input => input.TxAntennaType.Horizontal)
                .MaximumLength(7);
            this.RuleFor(input => input.TxAntennaType.Vertical)
                .MaximumLength(7);
        });
        this.RuleFor(input => input.TxAzimuth)
            .ExclusiveBetween(-100.0, 1_000.0);
        this.RuleFor(input => input.TxElevation)
            .ExclusiveBetween(-100.0, 1_000.0);
        this.RuleFor(input => input.TxAntennaHeight)
            .ExclusiveBetween(0, 10_000);
        this.RuleFor(input => input.RxAntennaHeight)
            .ExclusiveBetween(0, 10_000);
        this.RuleFor(input => input.TxPower)
            .ExclusiveBetween(0, 1000.00);
        this.RuleFor(input => input.TxFrequency)
            .GreaterThanOrEqualTo(1)
            .LessThan(100_000 * Math.Pow(10, 9));
        this.RuleFor(input => input.TxServiceAreaRadius)
            .InclusiveBetween(0, 9999);
        this.RuleFor(input => input.RxServiceAreaRadius)
            .InclusiveBetween(0, 9999);
        this.RuleFor(input => input.DistanceOverSea)
            .ExclusiveBetween(0, 10_000)
            .When(input => input.DistanceOverSea != null);
        this.RuleFor(input => input.RxFrequency)
            .GreaterThanOrEqualTo(1)
            .LessThan(100_000 * Math.Pow(10, 9));
        this.RuleFor(input => input.RxEmissionDesignation)
            .MaximumLength(9);
        this.RuleFor(input => input.TxEmissionDesignation)
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
        this.RuleFor(input => input.PermissibleFieldStrength)
            .ExclusiveBetween(-100, 1_000);
        this.RuleFor(input => input.FrequencyDifferenceCorrectionFactor)
            .ExclusiveBetween(-1_000, 10_000);
        this.RuleFor(input => input.TopoPath)
            .MaximumLength(63);
        this.RuleFor(input => input.BorderPath)
            .MaximumLength(63);
        this.RuleFor(input => input.MorphoPath)
            .MaximumLength(63);
    }
}
