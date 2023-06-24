using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LSL.Rebus.EfCore.SqlServer.Entities
{
    /// <summary>
    /// SagaIndex
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SagaIndex
    {
        /// <summary>
        /// SagaType
        /// </summary>
        /// <value></value>
        [MaxLength(40)]
        [Column("saga_type")]
        public string SagaType { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        /// <value></value>
        [MaxLength(200)]
        [Column("key")]
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        /// <value></value>
        [MaxLength(200)]
        [Column("value")]
        public string Value { get; set; }        

        /// <summary>
        /// SagaId
        /// </summary>
        /// <value></value>
        [Column("saga_id")]
        public Guid SagaId { get; set; }
            // var indexTable = source.Entity(indexTableName);

            // indexTable.Property<string>("saga_type").IsRequired().HasMaxLength(40);
            // indexTable.Property<string>("key").IsRequired().HasMaxLength(200);
            // indexTable.Property<string>("value").IsRequired().HasMaxLength(200);
            // indexTable.Property<Guid>("saga_id").IsRequired();

            // indexTable.HasKey(new[] { "key", "value", "saga_type" });
            // indexTable.HasIndex(new[] { "saga_id" });
            // indexTable.HasOne(dataTableName, $"{indexTable}To{dataTable}").WithOne().HasForeignKey(indexTableName, new[] { "saga_id" }).OnDelete(DeleteBehavior.Cascade);        
    }
}
