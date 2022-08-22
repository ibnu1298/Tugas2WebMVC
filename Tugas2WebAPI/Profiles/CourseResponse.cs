using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Profiles
{
    public class CourseResponse
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public List<CourseStudentDTO> Courses { get; set; }
    }
}
