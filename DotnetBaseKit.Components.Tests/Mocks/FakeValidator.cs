using FluentValidation;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeValidator : AbstractValidator<FakeBaseEntityWithData>
    {
        public FakeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}