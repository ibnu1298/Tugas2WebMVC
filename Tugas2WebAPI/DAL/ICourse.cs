using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DAL
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetCourseStudent();
    }
}
