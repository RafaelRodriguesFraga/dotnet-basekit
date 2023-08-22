using DotnetBoilerplate.Components.Domain.MongoDb.Entities.Base;

namespace TestApi.Domain.Entities
{
    public class Test : BaseEntity
    {
        public Test(string testString)
        {
            TestString = testString;
        }

        public string TestString { get; private set; }

        public override void Validate()
        {
        }
    }
}
