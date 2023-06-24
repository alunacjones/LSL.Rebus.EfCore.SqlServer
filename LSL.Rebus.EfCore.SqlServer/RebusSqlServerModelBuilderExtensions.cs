using System;
using Microsoft.EntityFrameworkCore;

namespace LSL.Rebus.EfCore.SqlServer
{
    /// <summary>
    /// RebusSqlServerModelBuilderExtensionsE
    /// </summary>
    public static class RebusSqlServerModelBuilderExtensions
    {
        /// <summary>
        /// Adds the necessary entities to implement the SqlServer Saga tables for Rebus
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dataTableName">The name for the data table (default is the Rebus SqlServer documentation's name)</param>
        /// <param name="indexTableName">The name for the index table (default is the Rebus SqlServer doumentation's name)</param>
        /// <returns>The source ModelBuilder</returns>
        public static ModelBuilder AddRebusSqlSagaTables(this ModelBuilder source, string dataTableName = "Sagas", string indexTableName = "SagaIndex")
        {
            var dataTable = source.Entity(dataTableName);

            dataTable.Property<Guid>("id").IsRequired();
            dataTable.Property<int>("revision").IsRequired();
            dataTable.Property<byte[]>("data").IsRequired();

            dataTable.HasKey(new[] { "id" });

            var indexTable = source.Entity(indexTableName);

            indexTable.Property<string>("saga_type").IsRequired().HasMaxLength(40);
            indexTable.Property<string>("key").IsRequired().HasMaxLength(200);
            indexTable.Property<string>("value").IsRequired().HasMaxLength(200);
            indexTable.Property<Guid>("saga_id").IsRequired();

            indexTable.HasKey(new[] { "key", "value", "saga_type" });
            indexTable.HasIndex(new[] { "saga_id" });
            indexTable.HasOne(dataTableName, $"{indexTable}To{dataTable}").WithOne().HasForeignKey(indexTableName, new[] { "saga_id" }).OnDelete(DeleteBehavior.Cascade);
            
            return source;
        }
    }
}