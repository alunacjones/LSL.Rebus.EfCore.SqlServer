using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LSL.Rebus.EfCore.SqlServer.Tests
{
    public class AddRebusTimeoutTableForSqlServerTests : ModelBuildingTest
    {
        [TestCase(typeof(TimeoutTestContext), "Timeouts")]
        [TestCase(typeof(TimeoutTestContextWithCustomTableNames), "CustomTimeoutsTable")]            
        public void GivenADbContextThatAddsTheTimeoutTable_ItShouldAddTheExpectedEntities(Type contextType, string expectedTableName)
        {
            TestTheContext(
                contextType, 
                new[] 
                {
                    new TestEntityModel
                    {
                        TableName = expectedTableName,
                        Fields = "body:varbinary(max),due_time:datetimeoffset,headers:nvarchar(max),id:bigint",
                        Keys = "Key: Timeout.Id PK",
                        Indexes = "Index: Timeout.DueTime"
                    }
                });
        }

        internal class TimeoutTestContext : DbContext
        {
            public TimeoutTestContext(DbContextOptions<TimeoutTestContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddRebusTimeoutTableForSqlServer();
            }
        }

        internal class TimeoutTestContextWithCustomTableNames : DbContext
        {
            public TimeoutTestContextWithCustomTableNames(DbContextOptions<TimeoutTestContextWithCustomTableNames> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddRebusTimeoutTableForSqlServer("CustomTimeoutsTable");
            }
        }        
    }    
}
