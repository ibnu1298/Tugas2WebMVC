using Microsoft.EntityFrameworkCore;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly DataContext _context;

        public EnrollmentDAL(DataContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var delete = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
                if (delete == null) throw new Exception($"Data dengan Id {id} tidak ditemukan");
                _context.Enrollments.Remove(delete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.OrderBy(e => e.EnrollmentID).ToListAsync();
            foreach (var item in results)
            {
                await _context.Courses.ToListAsync();
                await _context.Students.ToListAsync();
            }
            return results;
        }

        public Task<Enrollment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Enrollment>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                var course = await _context.Enrollments.FirstOrDefaultAsync(e => e.Course.CourseID == obj.CourseID);
                var student = await _context.Enrollments.FirstOrDefaultAsync(e => e.Student.ID == obj.StudentID);
                if (course == null && student == null) throw new Exception("Course & Student Tidak Ditemukan");
                if (course == null) throw new Exception("Course Tidak Ditemukan");
                if (student == null) throw new Exception("Student Tidak Ditemukan");
                var results = await _context.Enrollments.OrderBy(e => e.EnrollmentID).ToListAsync();
                foreach (var item in results)
                {
                    await _context.Courses.ToListAsync();
                    await _context.Students.ToListAsync();
                }
                _context.Enrollments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<Enrollment> Update(Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}
