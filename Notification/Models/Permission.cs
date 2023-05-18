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
    [Table("permission")]
    public class Permission
    {
        [Column("permission_id")]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Column("permission_name",TypeName ="varchar(50)")]
        public  string PermissionName { get; set; }
        [Column("description",TypeName ="varchar(200)")]
        public string? Description { get; set; }
        [JsonIgnore]
        public IList<RolePermission>? RolePermissions { get; set; }

    }
}
