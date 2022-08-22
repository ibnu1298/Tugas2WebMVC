using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class CreateEnrollmentStudentDTO
    {
        public int CourseID { get; set; }
        public Grade? Grade { get; set; }
    }
}
