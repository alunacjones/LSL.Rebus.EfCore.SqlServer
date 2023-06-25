using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;

namespace LSL.Rebus.EfCore.SqlServer.Tests
{
    public class AddRebusSagaTablesForSqlServerTests : ModelBuildingTest
    {
        [TestCase(typeof(TestContext), "Sagas", "SagaIndex")]
        [TestCase(typeof(TestContextWithCustomTableNames), "CustomSagaTable", "CustomSagaIndexTable")]
        public void GivenADbContextThatAddsTheRebusSagaTables_ItShouldAddTheExpectedEntities(Type contextType, string expectedDataTableName, string expectedIndexTableName)
        {
            TestTheContext(
                contextType, 
                new[] {
                new TestEntityModel
                {
                    TableName = expectedDataTableName,
                    Fields = "data:varbinary(max),id:uniqueidentifier,revision:int",
                    Keys = "Key: Saga.Id PK",
                },
                new TestEntityModel
                {
                    TableName = expectedIndexTableName,
                    Fields = "key:nvarchar(200),saga_id:uniqueidentifier,saga_type:nvarchar(40),value:nvarchar(200)" ,
                    Keys = "Key: SagaIndex.Key, SagaIndex.Value, SagaIndex.SagaType PK",
                    Indexes = "Index: SagaIndex.SagaId Unique",
                    ForeignKeys = "ForeignKey: SagaIndex {'SagaId'} -> Saga {'Id'} Unique"
                }
            });
        }
    }

    internal class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddRebusSagaTablesForSqlServer();
        }
    }

    internal class TestContextWithCustomTableNames : DbContext
    {
        public TestContextWithCustomTableNames(DbContextOptions<TestContextWithCustomTableNames> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddRebusSagaTablesForSqlServer("CustomSagaTable", "CustomSagaIndexTable");
        }
    }
}
