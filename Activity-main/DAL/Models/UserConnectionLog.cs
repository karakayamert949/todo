using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class UserConnectionLog
    {
        [Key]
        public int LogId { get; set; }

        public int UserId { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }
        public virtual User User { get; set; }
    }
}
