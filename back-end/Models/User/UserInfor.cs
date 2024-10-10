using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.User
{
    public class UserInfor
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public required Account Account { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DoB { get; set; }
        public string? PhoneNumber { get; set; }

    }
}