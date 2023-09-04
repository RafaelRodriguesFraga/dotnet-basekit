using TestApi.Domain.Entities;

namespace TestApi.Application.ViewModels
{
    public class TestApiViewModel
    {
        public string TestString { get; set; }

        public static implicit operator Test(TestApiViewModel test)
        {
            return new Test(test.TestString);
        }

        public static implicit operator TestSql(TestApiViewModel test)
        {
            return new TestSql(test.TestString);
        }
    }   
}
