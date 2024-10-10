namespace DTOs.UserDTOs
{
    public class RegisterDTO
    {
        public string AccountName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}