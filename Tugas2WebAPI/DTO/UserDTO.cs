namespace Tugas2WebAPI.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class CreateRoleDTO
    {
        public string Name { get; set; }
    }
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UserRoleDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
    }
    public class UserRoleListDTO
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public List<UserRoleGateDTO> UserRoles { get; set; }
    }
    public class UserRoleGateDTO
    {
        public RoleDTO Role { get; set; }
    }


}
