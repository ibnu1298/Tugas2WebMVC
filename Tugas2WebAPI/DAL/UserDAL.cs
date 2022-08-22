using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tugas2WebAPI.Models;
using Tugas2WebAPI.DAL;
using Microsoft.EntityFrameworkCore;

namespace Tugas2WebAPI.DAL
{
    public class UserDAL : IUser
    {
        private UserManager<CustomIdentityUser> _userManager;
        private AppSettings _appSettings;

        public UserDAL(UserManager<CustomIdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        public async Task<CustomIdentityUser> GetByNameUser(string email)
        {

            var result = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
            if (result == null)
                throw new Exception($"User dengan Nama {email} tidak ditemukan");
            return result;
        }
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var currUser = await _userManager.FindByNameAsync(username);
            var userResult = await _userManager.CheckPasswordAsync(currUser, password);
            if (!userResult)
                throw new Exception("Autentikasi gagal!, Email atau Password Salah");
            var role = await GetByNameUser(username);
            var user = new UserDTO{ Email = username };
            JwtSecurityToken token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(ClaimTypes.Role, $"{role.UserRoles.First().Role.Name}")},
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            user.Token = tokenHandler.WriteToken(token);
            return user;

        }

        public async Task<IEnumerable<CustomIdentityUser>> GetAll()
        {
            var results = await _userManager.Users.OrderBy(s => s.Email).ToListAsync();
            return results;
        }
        public async Task<CustomIdentityUser> GetEmail(string email)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (result == null) throw new Exception($"Email {email} Tidak ditemukan");
            return result;
        }

        public async Task Registration(CreateUserDTO user)
        {
            try
            {
                var newUser = new CustomIdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Hoby = user.Hoby,
                    Gender = user.Gender,
                    Image = user.Image,
                };
                var email = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (email != null)
                    throw new Exception($"Email {user.Email} sudah terdaftar");
          
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    var errors = result.Errors;
                    foreach (var error in errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n");
                    }
                    throw new Exception($"Error: {sb.ToString()}");
                }
                await _userManager.AddToRoleAsync(newUser, "user");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}