using Microsoft.AspNetCore.Identity;

namespace Tugas2WebAPI.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class CustomIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Hoby { get; set; }
        public Gender Gender { get; set; }
        public byte[]? Image { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    public class UserRole : IdentityUserRole<string>
    {
        public virtual CustomIdentityUser User { get; set; }
        public virtual CustomRole Role { get; set; }
    }

    public class CustomRole : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
