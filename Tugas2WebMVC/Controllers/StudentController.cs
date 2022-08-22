using Microsoft.AspNetCore.Mvc;
using Tugas2WebMVC.Models;
using Tugas2WebMVC.Services;

namespace Tugas2WebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }
        public async Task<IActionResult> Index(string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","User");
            }
            IEnumerable<Student> results;
            if (name == null)
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                results = await _student.GetAll(myToken);
            }
            else
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                results = await _student.GetByName(name, myToken);
            }
            return View(results);
        }
        public async Task<IActionResult> Details(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id, myToken);
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student obj)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _student.Insert(obj, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Berhasil menambahkan data Student {model.firstMidName} {model.lastName}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<span class='alert alert-danger'>Gagal Menambahkan Data {ex.Message}</span>";
                return View();
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id, myToken);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(StudentCourse obj)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                var model = await _student.Update(obj, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengubah data Student dengan Id {obj.id} menjadi </br> {obj.firstMidName} {obj.lastName}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");
            }
            var model = await _student.GetById(id, myToken);
            return View(model);
        }
        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                await _student.Delete(id, myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menghapus data Student dengan Id {id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<IActionResult> WithCourse(string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            IEnumerable<StudentCourse> results;
            if (name == null)
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                    if (myToken == null)
                    {
                        TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>Gagal Mengakses Student With Course</div>";
                        return RedirectToAction("Index", "Home");
                    }
                }
                results = await _student.GetStudentCourse(myToken);
            }
            else
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");
                }
                results = await _student.GetByNameFull(name, myToken);
            }
            return View(results);
        }
        public async Task<IActionResult> Paging(int id)
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
            Paging results = await _student.GetPaging(id, myToken);
            return View(results);
        }
    }
}
