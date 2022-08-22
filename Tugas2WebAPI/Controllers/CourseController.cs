using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2WebAPI.DAL;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;
using Tugas2WebAPI.Profiles;

namespace Tugas2WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CourseController(ICourse course, IMapper mapper, DataContext context)
        {
            _course = course;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            var results = await _course.GetAll();
            var courses = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return courses;
        }
        [AllowAnonymous]
        [HttpGet("form")]
        public async Task<IEnumerable<CourseDTO>> GetFform()
        {
            var results = await _course.GetAll();
            var courses = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return courses;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateCourseDTO create)
        {
            try
            {
                var newCourse = _mapper.Map<Course>(create);
                var result = await _course.Insert(newCourse);
                var course = _mapper.Map<CourseDTO>(result);

                return CreatedAtAction("Get", new { id = result.CourseID }, course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<CourseStudentDTO>> GetByName(string name)
        {
            var results = await _course.GetByName(name);
            var course = _mapper.Map<IEnumerable<CourseStudentDTO>>(results); ;
            return course;
        }
        [HttpGet("WithStudent")]
        public async Task<IEnumerable<CourseStudentDTO>> GetCourseStudent()
        {
            var results = await _course.GetCourseStudent();
            var course = _mapper.Map<IEnumerable<CourseStudentDTO>>(results); ;
            return course;
        }
        [HttpGet("Page/{page}")]
        public async Task<IEnumerable<CourseStudentDTO>> GetPage(int page)
        {
            var results = await _course.GetCourseStudent();
            var course = _mapper.Map<IEnumerable<CourseStudentDTO>>(results);
            var final = course.Skip((page - 1) * 3).Take(3).ToList();
            return final;
        }
        [HttpGet("Paging/{page}")]
        public async Task<ActionResult<List<CourseStudentDTO>>> GetPaging(int page)
        {
            var results = await _course.GetCourseStudent();
            var student = _mapper.Map<IEnumerable<CourseStudentDTO>>(results);
            if (_context.Students == null)
                return NotFound();
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Courses.Count() / pageResults);
            var courses = student.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            var response = new CourseResponse
            {
                Courses = courses,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CourseDTO courseDTO)
        {
            try
            {
                var resultCourse = _mapper.Map<Course>(courseDTO);
                var result = await _course.Update(resultCourse);
                return Ok(courseDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _course.Delete(id);
                return Ok($"Data Course dengan id {id} berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<CourseStudentDTO> GetById(int id)
        {
            var results = await _course.GetById(id);
            var student = _mapper.Map<CourseStudentDTO>(results); ;
            return student;
        }
    }
}
