namespace Tugas2WebMVC.Models
{
	public class StudentCourse
	{
        public int id { get; set; }
        public string firstMidName { get; set; }
        public string lastName { get; set; }
        public DateTime enrollmentDate { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
