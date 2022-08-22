namespace Tugas2WebMVC.Models
{
    public class Admin
    {
        public string Username { get; set; }
        public string Name { get; set; }
    }
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UserToRole
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }

    public class UserRole
    {
        public Role Role { get; set; }
    }

}
