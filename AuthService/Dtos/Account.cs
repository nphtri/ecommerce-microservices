using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos
{
    public class AccountReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
    }
    public class AccessTokenGenerateDto
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Only Alphabet or Number is accepted.")]
        public string Username { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must include at least 1 lowercase, 1 uppercase, 1 number, 1 special character and minimum length is 8.")]
        public string Password { get; set; }
    }
    public class AccessTokenResponseDto
    {
        public int AccountId { get; set; }
        public string AccessToken { get; set; }
    }
    public class RegisterDto
    {
        [Required]
        [RegularExpression("^([a-zA-Z0-9_]){3,}$", ErrorMessage = "Only Alphabet or Number or \"_\" is accepted. Minumum length is 3.")]
        public string Username { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must include at least 1 lowercase, 1 uppercase, 1 number, 1 special character and minimum length is 8.")]
        public string Password { get; set; }
        [Range(1, 3, ErrorMessage = "Role Id is required.")]
        public int RoleId { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        [Required]
        [MinLength(9)]
        [MaxLength(12)]
        public string Phone { get; set; }
    }
}