using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;

namespace LSL.Rebus.EfCore.SqlServer.Tests
{
    public class AddRebusSagaTablesForSqlServerTests
    {
        [TestCase(typeof(TestContext), "Sagas", "SagaIndex")]
        [TestCase(typeof(TestContextWithCustomTableNames), "CustomSagaTable", "CustomSagaIndexTable")]
        public void GivenADbContextThatAddsTheRebusSagaTables_ItShouldAddTheExpectedEntities(Type contextType, string expectedDataTableName, string expectedIndexTableName)
        {
            var closedBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(contextType);
            var optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(closedBuilderType);

            optionsBuilder.UseSqlServer("DataSource=:memory:");
            var sut = (DbContext)Activator.CreateInstance(contextType, optionsBuilder.Options);
            TestTheContext(sut, expectedDataTableName, expectedIndexTableName);
        }

        private void TestTheContext(DbContext sut, string expectedDataTableName, string expectedIndexTableName)
        {
            var ets = sut.Model.GetEntityTypes();

            ets.Select(e => new
            {
                TableName = e.GetTableName(),
                Fields = string.Join(",", e.GetProperties().OrderBy(p => p.GetColumnName()).Select(p => $"{p.GetColumnName()}:{p.GetColumnType()}")),
                Keys = string.Join(",", e.GetKeys().Select(k => k.ToString())),
                Indexes = string.Join(",", e.GetIndexes().Select(i => i.ToString())),
                Constraints = string.Join(",", e.GetCheckConstraints().Select(cc => cc.ToString())),
                ForeignKeys = string.Join(",", e.GetDeclaredForeignKeys().Select(fk => fk.ToString()))
            })
            .Should()
            .BeEquivalentTo(new[] {
                new
                {
                    TableName = expectedDataTableName,
                    Fields = "data:varbinary(max),id:uniqueidentifier,revision:int",
                    Keys = "Key: Saga.Id PK",
                    Indexes = "",
                    Constraints = "",
                    ForeignKeys = ""
                },
                new
                {
                    TableName = expectedIndexTableName,
                    Fields = "key:nvarchar(200),saga_id:uniqueidentifier,saga_type:nvarchar(40),value:nvarchar(200)" ,
                    Keys = "Key: SagaIndex.Key, SagaIndex.Value, SagaIndex.SagaType PK",
                    Indexes = "Index: SagaIndex.SagaId Unique",
                    Constraints = "",
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
