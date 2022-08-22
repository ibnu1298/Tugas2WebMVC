using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tugas2WebMVC.Models;
using Tugas2WebMVC.Services;

namespace Tugas2WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        private readonly IAdmin _admin;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUser user, IAdmin admin)
        {
            _user = user;
            _admin = admin;
        }
        public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Register()
		{
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(User obj)
		{
			try
			{
                var model = await _user.Register(obj);
				TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Halo, Selamat Datang {obj.FirstName} {obj.LastName}</div>";
				return RedirectToAction("Register");
			}
			catch (Exception ex)
			{
				ViewData["pesan"] = $"<span class='alert alert-danger'>Registrasi Gagal</span>";
				return View();
			}
		}
		public async Task<IActionResult> Login(string? returnUrl)
		{
			ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.returnUrl = returnUrl;
            }
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(Login obj, UserData user)
		{
			try
			{
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _user.Login(obj);
                var data = await _user.GetEmail(user.Email);
                var role = await _admin.GetByEmail(obj.Email, myToken);
                var fullname = ($"{data.FirstName} {data.LastName}");
                if (model != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", data.Email.ToLower()));
                    claims.Add(new Claim(ClaimTypes.Name, fullname));
                    claims.Add(new Claim("Gender", data.Gender));
                    claims.Add(new Claim("Address", data.Address));
                    claims.Add(new Claim("Image", data.Image));
                    claims.Add(new Claim("Role", role.UserRoles.FirstOrDefault().Role.Name));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Wellcome {data.FirstName} {data.LastName}</div>";
                    return RedirectToAction("Wellcome", "Home");
                }
                ViewData["pesan"] = $"<span class='alert alert-danger'>Login Gagal</span>";
                return View();
			}
			catch (Exception ex)
			{
				ViewData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>{ex.Message}</div>";
				return View();
			}
		}
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }

    }
}
