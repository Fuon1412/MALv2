using Microsoft.AspNetCore.Identity;

namespace Models.User
{
    public class Account : IdentityUser
    {
        public required string Username{get;set;}
        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = "active";
        public UserInfor? UserInfor { get; set; }
    }
}