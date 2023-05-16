using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notification.Domain.Models
{
    [Table("user")]
    public class User
    {
        [Column("user_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_name", TypeName = "varchar(100)")]
        public  string UserName { get; set; }
        [Column("password", TypeName = "varchar(50)")]
        public  string Password { get; set; }
        [Column("email", TypeName = "varchar(50)")]
        public  string EmailAddress { get; set; }
        [Column("full_name")]
        public  string FullName { get; set; }
        [JsonIgnore]
        public IList<UserRole>? UserRoles { get; set; }
    }
}
