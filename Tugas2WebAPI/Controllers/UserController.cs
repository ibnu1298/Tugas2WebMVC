using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2WebAPI.DAL;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public UserController(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> Registration(CreateUserDTO userDTO)
		{
			try
			{
				await _user.Registration(userDTO);
				return Ok($"Registrasi user {userDTO.Email} Berhasil");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        [AllowAnonymous]
        [HttpGet]
		public async Task<IEnumerable<ReadUserDTO>> GetAll()
        {
			var user = await _user.GetAll();
			var userDTO = _mapper.Map<IEnumerable<ReadUserDTO>>(user);
			return userDTO;
        }
		[AllowAnonymous]
        [HttpGet("{Email}")]
		public async Task<ReadUserDTO> GetEmail(string Email)
        {
			var user = await _user.GetEmail(Email);
			var userDTO = _mapper.Map<ReadUserDTO>(user);
			return userDTO;
        }

		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Authenticate(LoginUserDTO createUserDTO)
		{
			try
			{
				var user = await _user.Authenticate(createUserDTO.Email ,createUserDTO.Password);
				if (user == null)
					return BadRequest("Username or Password Wrong");
				return Ok(user);
			}
			catch (Exception ex)
			{
				return BadRequest($"Error: {ex.Message}");
			}
		}
	}
}
