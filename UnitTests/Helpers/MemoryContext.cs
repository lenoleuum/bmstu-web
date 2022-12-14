using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mbti_web;
using UnitTests.ObjectMother;

namespace UnitTests.Helpers
{
    public class MemoryContext
    {
        public mbti_dbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<mbti_dbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;

            return new mbti_dbContext(options);
        }
    }
}
