using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAll(string token);
        Task<IEnumerable<StudentCourse>> GetStudentCourse(string token);
        Task<Paging> GetPaging(int id, string token);
        Task<IEnumerable<StudentCourse>> GetByNameFull(string name, string? token);
        Task<IEnumerable<Student>> GetByName(string name, string? token);
        Task<StudentCourse> GetById(int id, string? token);
        Task<Student> Insert(Student obj, string? token);
        Task<StudentCourse> Update(StudentCourse obj, string? token);
        Task Delete(int id, string? token);
    }
}
