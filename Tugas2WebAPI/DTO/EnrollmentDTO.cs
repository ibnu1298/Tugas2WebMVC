using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }
        public Grade? Grade { get; set; }
        public CourseDTO Course { get; set; }
    }
}
