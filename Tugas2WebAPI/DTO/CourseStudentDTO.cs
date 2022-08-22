namespace Tugas2WebAPI.DTO
{
    public class CourseStudentDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<EnrollmentStudentDTO> Enrollments { get; set; }
    }
}
