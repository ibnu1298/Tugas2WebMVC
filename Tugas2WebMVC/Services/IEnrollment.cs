using Tugas2WebMVC.Models;

namespace Tugas2WebMVC.Services
{
    public interface IEnrollment
    {
        Task<IEnumerable<EnrollmentCS>> GetAll();
        Task<IEnumerable<EnrollmentCS>> GetByGrade(string name);
        Task<EnrollmentCS> Insert(EnrollmentCS obj,string token);
        Task<EnrollmentCS> Update(EnrollmentCS obj);
        Task Delete(int id);
    }
}
