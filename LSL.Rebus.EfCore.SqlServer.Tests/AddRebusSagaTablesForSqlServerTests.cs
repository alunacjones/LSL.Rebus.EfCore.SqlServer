using System;
using Microsoft.EntityFrameworkCore;
using LSL.Rebus.EfCore.SqlServer;
using NUnit.Framework;
using FluentAssertions;

namespace LSL.Rebus.EfCore.SqlServer.Tests
{
    public class AddRebusSagaTablesForSqlServerTests
    {
        [Test]
        public void GivenADbContextThatAddsTheRebusSagaTables_ItShouldAddTheExpectedEntities()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();

            optionsBuilder.UseSqlServer("DataSource=:memory:");            
            var sut = new TestContext(optionsBuilder.Options);
            
            sut.Should().NotBeNull();
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
}
