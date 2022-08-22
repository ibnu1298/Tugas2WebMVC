using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2WebAPI.DAL;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEnrollment _enroll;

        public EnrollmentController(IMapper mapper, IEnrollment enroll)
        {
            _mapper = mapper;
            _enroll = enroll;
        }

        //Dalam Membuat Insert Data yang akan mengembalikan data yang telah di Insert pastikan pada Contoller ini juga sudah tersedia HttpGet untuk menampilkannya
        [HttpPost] 
        public async Task<ActionResult> Post(CreateEnrollmentDTO create)
        {
            try
            {
                var newEnroll = _mapper.Map<Enrollment>(create);
                var result = await _enroll.Insert(newEnroll);
                var enrollment = _mapper.Map<EnrollmentCourseStudentDTO>(result);
                return CreatedAtAction("Get", new { id = result.EnrollmentID }, enrollment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IEnumerable<EnrollmentCourseStudentDTO>> Get()
        {
            var results = await _enroll.GetAll();
            var enroll = _mapper.Map<IEnumerable<EnrollmentCourseStudentDTO>>(results);
            return enroll;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _enroll.Delete(id);
                return Ok($"Data Enrollment dengan id {id} berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
