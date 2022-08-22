using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class EnrollmentStudentDTO
    {
        public Grade? Grade { get; set; }
        public StudentDTO Student { get; set; }
    }
}
