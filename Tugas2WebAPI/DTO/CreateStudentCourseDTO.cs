namespace Tugas2WebAPI.DTO
{
    public class CreateStudentCourseDTO
    {
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<CreateEnrollmentStudentDTO> Enrollments { get; set; }
    }
}
