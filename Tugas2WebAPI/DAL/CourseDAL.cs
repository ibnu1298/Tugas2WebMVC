using Microsoft.EntityFrameworkCore;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.DAL
{
    public class CourseDAL : ICourse
    {
        private readonly DataContext _context;

        public CourseDAL(DataContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteCourse = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == id);
                if (deleteCourse == null) throw new Exception($"Data Student dengan Id {id} tidak ditemukan");
                _context.Courses.Remove(deleteCourse);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(s => s.Title).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            
            var result = await _context.Courses.Include(c => c.Enrollments).FirstOrDefaultAsync(s => s.CourseID == id);
            if (result == null) throw new Exception($"Tidak ada data dengan Id = {id}");
            foreach (var item in result.Enrollments)
            {
                await _context.Enrollments.Include(e => e.Student).ToListAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Course>> GetByName(string name)
        {
            var results = await _context.Courses.Include(c => c.Enrollments).Where(c => c.Title.Contains(name)).OrderBy(s => s.CourseID).ToListAsync();
            foreach (var item in results)
            {
                await _context.Enrollments.Include(e => e.Student).ToListAsync();
            }
            return results;
        }

        public async Task<IEnumerable<Course>> GetCourseStudent()
        {
            var results = await _context.Courses.Include(c => c.Enrollments).ToListAsync();
            foreach (var enroll in results)
            {
                await _context.Enrollments.Include(e => e.Student).ToListAsync();
            }
            return results;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                var title = await _context.Courses.FirstOrDefaultAsync(c => c.Title == obj.Title);
                var id = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == obj.CourseID);
                if (id != null) throw new Exception($"Id {obj.CourseID} Sudah terdaftar");
                if (title != null) throw new Exception("Course Ini Sudah terdaftar");
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Course> Update(Course obj)
        {
            try
            {
                var update = await _context.Courses
                    .Include(s => s.Enrollments)
                    .FirstOrDefaultAsync(s => s.CourseID == obj.CourseID);
                if (update == null) throw new($"Data dengan Id {obj.CourseID} Tidak ditemukan");
                update.Title = obj.Title;
                update.Credits = obj.Credits;
                await _context.SaveChangesAsync();
                return obj;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
