using FluentValidation;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeValidatorMongo : AbstractValidator<FakeBaseEntityMongoWithData>
    {
        public FakeValidatorMongo()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}