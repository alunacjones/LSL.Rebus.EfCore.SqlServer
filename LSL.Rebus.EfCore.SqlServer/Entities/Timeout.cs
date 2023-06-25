using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LSL.Rebus.EfCore.SqlServer.Entities
{
    /// <summary>
    /// Timeout
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Timeout
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <value></value>
        [Key]
        [Column("id")]        
        public long Id { get; set; }        

        /// <summary>
        /// DueTime
        /// </summary>
        /// <value></value>
        [Column("due_time")]
        public DateTimeOffset DueTime { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        /// <value></value>
        [Column("headers")]
        public string Headers { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        /// <value></value>
        [Column("body")]
        public byte[] Body { get; set; }
    }
}
