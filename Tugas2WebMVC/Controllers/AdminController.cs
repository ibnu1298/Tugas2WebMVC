using Microsoft.AspNetCore.Mvc;
using Tugas2WebMVC.Models;
using Tugas2WebMVC.Services;

namespace Tugas2WebMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            IEnumerable<Role> results = await _admin.GetAll(myToken);
            return View(results);
        }
        public async Task<IActionResult> UserRole()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            IEnumerable<UserToRole> results = await _admin.GetUserRole(myToken);
            return View(results);
        }
        public async Task<IActionResult> Create()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Role obj)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _admin.Insert(obj,myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Berhasil menambahkan data Student {model.Name}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>Gagal Menambahkan Data {ex.Message}</span>";
                return View();
            }
        }
        public async Task<IActionResult> AddRole()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(Admin obj)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _admin.Admin(obj, myToken);
            var role = await _admin.GetByName(obj.Name, myToken);
            var user = await _admin.GetByEmail(obj.Username, myToken);
            TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Berhasil menambahkan Role {role.Name} Ke User {user.FirstName} {user.Lastname}</div>";
            return RedirectToAction("UserRole");
        }
    }
}
