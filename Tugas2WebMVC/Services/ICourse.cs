using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public interface ICourse
    {
        Task<IEnumerable<Course>> GetAll(string token);
        Task<IEnumerable<CourseStudent>> GetCourseStudent(string token);
        Task<PagingCourse> GetPaging(int id, string token);
        Task<IEnumerable<Course>> GetByName(string name, string token);
        Task<IEnumerable<CourseStudent>> GetByNameFull(string name, string token);
        Task<CourseStudent> GetById(int id, string token);
        Task<Course> Insert(Course obj, string token);
        Task<CourseStudent> Update(CourseStudent obj, string token);
        Task Delete(int id, string token);
    }
}
