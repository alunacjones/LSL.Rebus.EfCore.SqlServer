using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LSL.Rebus.EfCore.SqlServer.Entities
{
    /// <summary>
    /// Outbox
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Outbox
    {
        /// <summary>
        /// Key
        /// </summary>
        /// <value></value>
        [Key]        
        public long Id { get; set; }

        /// <summary>
        /// CorrelationId
        /// </summary>
        /// <value></value>
        [MaxLength(16)]
        public string CorrelationId { get; set; }

        /// <summary>
        /// MessageId
        /// </summary>
        /// <value></value>
        [MaxLength(255)]
        public string MessageId { get; set; }

        /// <summary>
        /// SourceQueue
        /// </summary>
        /// <value></value>
        [MaxLength(255)]        
        public string SourceQueue { get; set; }        

        /// <summary>
        /// DestinationAddress
        /// </summary>
        /// <value></value>
        [MaxLength(255)]
        [Required]
        public string DestinationAddress { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        /// <value></value>
        public string Headers { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        /// <value></value>
        public byte[] Body { get; set; } 
        
        /// <summary>
        /// Sent
        /// </summary>
        /// <value></value>
        [Required]
        [DefaultValue(false)]
        public bool Sent { get; set; }
    }
}
