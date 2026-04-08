namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class BuildLegacyInputStringPointToLineInputValidator : BuildLegacyInputStringInputValidator<BuildLegacyInputStringPointToLineInput>
{
    public BuildLegacyInputStringPointToLineInputValidator() =>
        this.RuleFor(input => input.MaxCrossBorderRange)
            .ExclusiveBetween(-100, 1_000);
}
