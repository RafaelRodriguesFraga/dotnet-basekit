using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
