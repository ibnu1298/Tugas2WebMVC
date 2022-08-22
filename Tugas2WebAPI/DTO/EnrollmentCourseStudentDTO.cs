using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class EnrollmentCourseStudentDTO
    {
        public int EnrollmentID { get; set; }
        public Grade? Grade { get; set; }
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
    }
}
