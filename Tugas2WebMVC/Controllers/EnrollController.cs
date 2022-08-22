using Microsoft.AspNetCore.Mvc;
using Tugas2WebMVC.Models;
using Tugas2WebMVC.Services;

namespace Tugas2WebMVC.Controllers
{
    public class EnrollController : Controller
    {
        private readonly IEnrollment _enroll;

        public EnrollController(IEnrollment enroll)
        {
            _enroll = enroll;
        }
        public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(EnrollmentCS obj)
		{
			try
			{
				if (!User.Identity.IsAuthenticated)
				{
					return RedirectToAction("Login", "User");
				}
				string myToken = string.Empty;
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
				{
					myToken = HttpContext.Session.GetString("token");
				}
				var model = await _enroll.Insert(obj, myToken);
				TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Berhasil Mendaftarkan data Student {model.student.firstMidName} {model.student.lastName} ke Course {model.course.title} Grade {model.grade}</div>";
				return RedirectToAction("WithCourse","Student");
			}
			catch (Exception ex)
			{
				ViewData["pesan"] = $"<span class='alert alert-danger'>Gagal Menambahkan Data {ex.Message}</span>";
				return View();
			}
		}
	}
}
