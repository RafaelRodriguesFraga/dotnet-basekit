using DotnetBaseKit.Components.Domain.MongoDb;
using DotnetBaseKit.Components.Domain.MongoDb.Entities.Base;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeBaseEntity : BaseEntity
    {
        public override void Validate()
        {

        }
    }

    public class FakeBaseEntityWithData : BaseEntity
    {

        public string Name { get; private set; }

        public FakeBaseEntityWithData(string name)
        {
            Name = name;
        }
        public override void Validate()
        {
            var validator = new FakeValidator();
            var result = validator.Validate(this);

            this.AddNotifications(result);
        }
    }
}
