using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DAL
{
    public interface IUser
    {
        Task Registration(CreateUserDTO user);
        Task<UserDTO> Authenticate(string username, string password);
        Task<IEnumerable<CustomIdentityUser>> GetAll();
        Task<CustomIdentityUser> GetEmail(string email);
        Task<CustomIdentityUser> GetByNameUser(string email);
    }
}
