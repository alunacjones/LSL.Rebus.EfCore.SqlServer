using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LSL.Rebus.EfCore.SqlServer.Tests
{
    public class AddRebusOutboxTableForSqlServerTests : ModelBuildingTest
    {
        [TestCase(typeof(OutboxTestContext), "Outbox")]
        [TestCase(typeof(OutboxTestContextWithCustomTableNames), "CustomOutboxTable")]            
        public void GivenADbContextThatAddsTheOutboxTable_ItShouldAddTheExpectedEntities(Type contextType, string expectedTableName)
        {
            TestTheContext(
                contextType, 
                new[] 
                {
                    new TestEntityModel
                    {
                        TableName = expectedTableName,
                        Fields = "Body:varbinary(max),CorrelationId:nvarchar(16),DestinationAddress:nvarchar(255),Headers:nvarchar(max),Id:bigint,MessageId:nvarchar(255),Sent:bit,SourceQueue:nvarchar(255)",
                        Keys = "Key: Outbox.Id PK"
                    }
                });
        }

        internal class OutboxTestContext : DbContext
        {
            public OutboxTestContext(DbContextOptions<OutboxTestContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddRebusOutboxTableForSqlServer();
            }
        }

        internal class OutboxTestContextWithCustomTableNames : DbContext
        {
            public OutboxTestContextWithCustomTableNames(DbContextOptions<OutboxTestContextWithCustomTableNames> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.AddRebusOutboxTableForSqlServer("CustomOutboxTable");
            }
        }        
    }    
}
