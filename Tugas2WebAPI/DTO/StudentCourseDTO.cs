namespace Tugas2WebAPI.DTO
{
    public class StudentCourseDTO
    {
        public int ID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<EnrollmentDTO> Enrollments { get; set; }
    }
}
