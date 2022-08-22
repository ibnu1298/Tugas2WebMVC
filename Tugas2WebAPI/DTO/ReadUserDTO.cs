using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class ReadUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Hoby { get; set; }
        public Gender Gender { get; set; }
        public byte[]? Image { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
