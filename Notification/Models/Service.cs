using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Domain.Models
{
    [Table("service")]
    public class Service
    {
        [Column("service_id")]
        [Key]
        public int Id { get; set; }
        [Column("service_name")]
        public required string ServiceName { get; set; }

        [Column("ip",TypeName ="varchar(200)")]
        public required string ServiceIp { get; set; } 

        private ICollection<Message> Messages { get; set; }

    }
}
