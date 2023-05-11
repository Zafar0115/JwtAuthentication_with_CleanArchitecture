using Notification.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notification.Domain.Models
{
    [Table("message")]
    public class Message
    {
        [Column("message_id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("status")]
        public Status NotificationStatus { get; set; }
        [ForeignKey("service_id")]
        public Service Service { get; set; }
        [Column("date", TypeName = "timestamp")]
        public DateTime Date { get; set; }
    }
}