using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DTO
{
    public class CreateEnrollmentDTO
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
    }
}
