namespace Tugas2WebMVC.Models
{
    public class Paging
    {
        public int pages { get; set; }
        public int currentPage { get; set; }
        public List<StudentCourse> students { get; set; }
    }
    public class PagingCourse
    {
        public int pages { get; set; }
        public int currentPage { get; set; }
        public List<CourseStudent> courses { get; set; }
    }
}
