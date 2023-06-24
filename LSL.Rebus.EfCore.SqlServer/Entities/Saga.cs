using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LSL.Rebus.EfCore.SqlServer.Entities
{
    /// <summary>
    /// The Entity to represent to Sagas table for Rebus SqlServer Sagas
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Saga
    {
        /// <summary>
        /// The id
        /// </summary>
        /// <value></value>
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The revision
        /// </summary>
        /// <value></value>
        [Column("revision")]
        public int Revision { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        /// <value></value>
        [Column("data")]
        public byte[] Data { get; set; }
    }
}
