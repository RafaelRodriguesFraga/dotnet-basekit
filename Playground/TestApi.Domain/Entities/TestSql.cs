using DotnetBaseKit.Components.Domain.Sql.Entities.Base;

namespace TestApi.Domain.Entities
{
    public class TestSql : BaseEntity
    {
        public string TestString { get; set; }
        public TestSql()
        {
            
        }
        public TestSql(string testString)
        {
            TestString = testString;
        }

        public void Update(string testString)
        {
            TestString = string.IsNullOrEmpty(testString) ? TestString : testString;
        }
       

        public override void Validate()
        {
        }
    }
}
