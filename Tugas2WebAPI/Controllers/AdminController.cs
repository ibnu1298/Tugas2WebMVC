using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tugas2WebAPI.DAL;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tugas2WebAPI.Controllers
{
    [Authorize(Roles = "admin,developer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public AdminController(IAdmin admin, IMapper mapper, UserManager<CustomIdentityUser> userManager)
        {
            _admin = admin;
            _mapper = mapper;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<RoleDTO>> Get()
        {
            var results = await _admin.GetRole();
            var role = _mapper.Map<IEnumerable<RoleDTO>>(results);
            return role;
        }
        [HttpGet("UserRole")]
        public async Task<IEnumerable<UserRoleListDTO>> GetUserRole()
        {
            var results = await _admin.GetUserRole();
            var role = _mapper.Map<IEnumerable<UserRoleListDTO>>(results);
            return role;
        }
        [HttpGet("{name}")]
        public async Task<RoleDTO> GetByName(string name)
        {
            var results = await _admin.GetByName(name);
            var role = _mapper.Map<RoleDTO>(results);
            return role;
        }
        [AllowAnonymous]
        [HttpGet("User/{name}")]
        public async Task<UserRoleListDTO> GetByNameUser(string name)
        {
            var results = await _admin.GetByNameUser(name);
            var userRole = _mapper.Map<UserRoleListDTO>(results);
            return userRole;
        }
        [HttpPost]
        public async Task<ActionResult> Post(CreateRoleDTO create)
        {
            try
            {
                await _admin.CreateRole(create);
                return Ok($"Role {create.Name} Berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(RoleDTO obj)
        {
            try
            {
                var result = _mapper.Map<CustomRole>(obj);
                await _admin.Update(result);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _admin.Delete(id);
                return Ok($"Role berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UserRole")]
        public async Task<IActionResult> AddUserToRole(UserRoleDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            try
            {
                await _userManager.AddToRoleAsync(user, model.Name);
                return Ok("Berhasil menambahkan Role");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(UserRoleDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            try
            {
                await _userManager.RemoveFromRoleAsync(user, model.Name);
                return Ok($"Berhasil Menghapus Role Pada User {model.Username}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
