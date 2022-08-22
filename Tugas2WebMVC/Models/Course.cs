namespace Tugas2WebMVC.Models
{
	public class Course
	{
		public int courseID { get; set; }
		public string title { get; set; }
		public int credits { get; set; }
	}
	public class CourseStudent
	{
		public int courseID { get; set; }
		public string title { get; set; }
		public int credits { get; set; }
		public List<EnrollmentStudent> enrollments { get; set; }
	}

}
