namespace back_end.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; } = String.Empty;
        public string NewPassword { get; set; } = String.Empty;
        public string ConfirmNewPassword { get; set; } = String.Empty;
    }
}