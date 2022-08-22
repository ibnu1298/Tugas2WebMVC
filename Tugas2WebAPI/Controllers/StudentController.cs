using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tugas2WebAPI.DAL;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;
using Tugas2WebAPI.Profiles;

namespace Tugas2WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public StudentController(IStudent student, IMapper mapper, DataContext context)
        {
            _student = student;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            var results = await _student.GetAll();
            var student = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return student;
        }
        [AllowAnonymous]
        [HttpGet("OName")]
        public async Task<IEnumerable<StudentDTO>> GetOName()
        {
            var results = await _student.GetAllName();
            var student = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return student;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateStudentDTO create)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(create);
                var result = await _student.Insert(newStudent);
                var student = _mapper.Map<StudentDTO>(result);

                return CreatedAtAction("Get", new { id = result.ID }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddWithCourse")]
        public async Task<ActionResult> AddStudentCourse(CreateStudentCourseDTO CreateDTO)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(CreateDTO);
                var result = await _student.Insert(newStudent);
                var student = _mapper.Map<StudentCourseDTO>(result);
                return CreatedAtAction("Get", new { id = result.ID }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize]
        [HttpGet("ByName/{name}")]
        public async Task<IEnumerable<StudentCourseDTO>> GetByName(string name)
        {
            var results = await _student.GetByName(name);
            var student = _mapper.Map<IEnumerable<StudentCourseDTO>>(results); ;
            return student;
        }
        //[Authorize]
        [HttpGet("WithCourse")]
        public async Task<IEnumerable<StudentCourseDTO>> GetStudentCourse()
        {
            var results = await _student.GetStudentEnrollments();
            var student = _mapper.Map<IEnumerable<StudentCourseDTO>>(results); ;
            return student;
        } 
        [HttpGet("{id}")]
        public async Task<StudentCourseDTO> GetById(int id)
        {
            var results = await _student.GetById(id);
            var student = _mapper.Map<StudentCourseDTO>(results); ;
            return student;
        }
        [HttpGet("Page/{page}")]
        public async Task<IEnumerable<StudentCourseDTO>> GetPage(int page)
        {
            var results = await _student.GetStudentEnrollments();
            var student = _mapper.Map<IEnumerable<StudentCourseDTO>>(results);
            var final = student.Skip((page - 1) * 3).Take(3).ToList();
            return final;
        }
        [HttpGet("Paging/{page}")]
        public async Task<ActionResult<List<StudentCourseDTO>>> GetPaging(int page)
        {
            var results = await _student.GetStudentEnrollments();
            var student = _mapper.Map<IEnumerable<StudentCourseDTO>>(results);
            if (_context.Students == null)
                return NotFound();
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Students.Count() / pageResults);
            var students = student.Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();

            var response = new StudentResponse
            {
                Students = students,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentDTO studentDTO)
        {
            try
            {
                var resultStudent = _mapper.Map<Student>(studentDTO);
                var result = await _student.Update(resultStudent);
                return Ok(studentDTO);

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
                await _student.Delete(id);
                return Ok($"Data Student dengan id {id} berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
