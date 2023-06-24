using System;
using LSL.Rebus.EfCore.SqlServer.Entities;
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
        public static ModelBuilder AddRebusSagaTablesForSqlServer(this ModelBuilder source, string dataTableName = "Sagas", string indexTableName = "SagaIndex")
        {
            var dataTable = source.Entity<Saga>().ToTable(dataTableName);            

            var indexTable = source.Entity<SagaIndex>().ToTable(indexTableName);
            indexTable.HasKey(e => new { e.Key, e.Value, e.SagaType });
            indexTable.HasOne<Saga>().WithOne().OnDelete(DeleteBehavior.Cascade);
            
            return source;
        }
    }
}