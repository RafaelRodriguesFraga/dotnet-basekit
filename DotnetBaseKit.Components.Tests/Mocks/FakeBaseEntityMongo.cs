using DotnetBaseKit.Components.Domain.MongoDb;
using DotnetBaseKit.Components.Domain.MongoDb.Entities.Base;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeBaseEntityMongo : BaseEntity
    {
        public FakeBaseEntityMongo(Guid id, DateTime createdAt)
            : base(id, createdAt)
        {
        }

        public override void Validate()
        {

        }
    }

    public class FakeBaseEntityMongoWithData : BaseEntity
    {

        public string Name { get; private set; }

        public FakeBaseEntityMongoWithData(string name)
        {
            Name = name;
        }
        public override void Validate()
        {
            var validator = new FakeValidatorMongo();
            var result = validator.Validate(this);

            this.AddNotifications(result);
        }
    }
}
