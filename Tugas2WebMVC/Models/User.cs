using System.ComponentModel.DataAnnotations;

namespace Tugas2WebMVC.Models
{
    public enum gender
    {
        Pria, Wanita
    }
    public class User
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Hoby { get; set; }
        [Required]
        public gender Gender { get; set; }
        public string? Image { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation Password")]
        [Compare("Password", ErrorMessage = "Password Tidak Sama")]
        public string ConfirmPassword { get; set; }
    }
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Hoby { get; set; }
        public string Gender { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
    }
}
