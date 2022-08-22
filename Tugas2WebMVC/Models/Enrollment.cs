namespace Tugas2WebMVC.Models
{
    public enum grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int enrollmentID { get; set; }
        public grade grade { get; set; }
        public Course course { get; set; }
    }
    public class EnrollmentStudent
    {
        public grade grade { get; set; }
        public Student student { get; set; }
    }
}
