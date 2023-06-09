﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notification.Domain.Models
{
    [Table("service")]
    public class Service
    {
        [Column("service_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("ip",TypeName ="varchar(200)")]
        public string ServiceIp { get; set; }
        [JsonIgnore]
        public ICollection<Message>? Messages { get; set; }

    }
}
