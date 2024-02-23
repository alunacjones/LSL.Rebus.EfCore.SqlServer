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
        public static ModelBuilder AddRebusSagaTablesForSqlServer(this ModelBuilder source, string dataTableName = TableNames.Sagas, string indexTableName = TableNames.SagaIndex)
        {
            var dataTable = source.Entity<Saga>().ToTable(dataTableName);            

            var indexTable = source.Entity<SagaIndex>().ToTable(indexTableName);
            indexTable.HasKey(e => new { e.Key, e.Value, e.SagaType });
            indexTable.HasOne<Saga>().WithOne().OnDelete(DeleteBehavior.Cascade);
            
            return source;
        }

        /// <summary>
        /// Adds the necessary entities to implement the SqlServer Timeout table for Rebus
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableName">The name for the table (default is the Rebus SqlServer documentation's name)</param>
        /// <returns>The source ModelBuilder</returns>
        public static ModelBuilder AddRebusTimeoutTableForSqlServer(this ModelBuilder source, string tableName = TableNames.Timeouts)
        {
            var table = source.Entity<Timeout>().ToTable(tableName);
            table.HasIndex(e => e.DueTime);

            return source;
        }

        /// <summary>
        /// Adds the necessary entities to implement the SqlServer Outbox table for Rebus
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tableName">The name for the table (default is the Rebus SqlServer documentation's name)</param>
        /// <returns>The source ModelBuilder</returns>
        public static ModelBuilder AddRebusOutboxTableForSqlServer(this ModelBuilder source, string tableName = TableNames.Outbox)
        {
            var table = source.Entity<Outbox>().ToTable(tableName);

            return source;
        }
    }
}
