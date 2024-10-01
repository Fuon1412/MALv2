namespace api.Models.User
{
    public class Information
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public Account? Account { get; set; }
    }
}