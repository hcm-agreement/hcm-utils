namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class LegacyPointToLineInputValidator : LegacyInputValidator<LegacyPointToLineInput>
{
    public LegacyPointToLineInputValidator() =>
        this.RuleFor(input => input.MaxCrossBorderRange)
            .ExclusiveBetween(-100, 1_000);
}
