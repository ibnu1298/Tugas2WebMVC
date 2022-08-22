using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DAL
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetStudentEnrollments();
        Task<IEnumerable<Student>> GetAllName();
        Task<StudentCourseDTO> UpdateFull(StudentCourseDTO obj, EnrollmentDTO enrollObj, CourseDTO courseObj);
    }
}
