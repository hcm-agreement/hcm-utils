namespace HCMUtils.Validators;

using FluentValidation;
using HCMUtils.Types;

public class LegacyPointToLineInputStringValidator : LegacyInputStringInputValidator<LegacyPointToLineInputString>
{
    public LegacyPointToLineInputStringValidator() =>
        this.RuleFor(input => input.MaxCrossBorderRange)
            .ExclusiveBetween(-100, 1_000);
}
