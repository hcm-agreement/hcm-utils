namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class BuildLegacyStringPointToLineInputValidator : BuildLegacyStringInputValidator<BuildLegacyInputStringPointToLineInput>
{
    public BuildLegacyStringPointToLineInputValidator() =>
        this.RuleFor(input => input.MaxCrossBorderRange)
            .ExclusiveBetween(-100, 1_000);
}
