using DotnetBaseKit.Components.Domain.Sql;
using DotnetBaseKit.Components.Domain.Sql.Entities.Base;

namespace DotnetBaseKit.Components.Tests.Mocks
{
    public class FakeBaseEntitySql : BaseEntity
    {
        public override void Validate()
        {

        }
    }

    public class FakeBaseEntitySqlWithData : BaseEntity
    {

        public string Name { get; private set; }

        public FakeBaseEntitySqlWithData(string name)
        {
            Name = name;
        }
        public override void Validate()
        {
            var validator = new FakeValidatorSql();
            var result = validator.Validate(this);

            this.AddNotifications(result);
        }
    }
}
