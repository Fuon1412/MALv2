using System.ComponentModel.DataAnnotations;
using Models.User;
namespace Models.User
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = "active";
        public UserInfor? UserInfor { get; set; }
    }
}