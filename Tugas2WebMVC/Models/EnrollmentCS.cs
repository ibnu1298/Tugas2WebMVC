namespace Tugas2WebMVC.Models
{
    public class EnrollmentCS
    {
        public int enrollmentID { get; set; }
        public int courseID { get; set; }
        public int studentID { get; set; }
        public grade grade { get; set; }
        public Course course { get; set; }
        public Student student { get; set; }
    }
}
