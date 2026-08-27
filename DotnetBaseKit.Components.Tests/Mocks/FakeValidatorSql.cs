using FluentValidation;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeValidatorSql : AbstractValidator<FakeBaseEntitySqlWithData>
    {
        public FakeValidatorSql()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}