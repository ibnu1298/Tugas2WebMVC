using Microsoft.EntityFrameworkCore;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;
using Tugas2WebAPI.Profiles;

namespace Tugas2WebAPI.DAL
{
    public class StudentDAL : IStudent
    {
        private readonly DataContext _context;

        public StudentDAL(DataContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteStudent = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                if (deleteStudent == null) throw new Exception($"Data Student dengan Id {id} tidak ditemukan");
                _context.Students.Remove(deleteStudent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.ID).ToListAsync();
            return results;
        } 
        public async Task<IEnumerable<Student>> GetAllName()
        {
            var results = await _context.Students.OrderBy(s => s.FirstMidName).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.Include(s => s.Enrollments).FirstOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Tidak ada data dengan Id = {id}");
            foreach (var enroll in result.Enrollments)
            {
                await _context.Enrollments.Include(e => e.Course).ToListAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Student>> GetByName(string name)
        {
            var results = await _context.Students.Include(s => s.Enrollments).Where(s => s.FirstMidName.Contains(name) || s.LastName.Contains(name)).OrderBy(s => s.ID).ToListAsync();
            foreach (var enroll in results)
            {
                await _context.Enrollments.Include(e => e.Course).ToListAsync();
            }
            return (results);
        }

        public async Task<IEnumerable<Student>> GetStudentEnrollments()
        {
            var results = await _context.Students.Include(s => s.Enrollments).ToListAsync();
            foreach (var enroll in results)
            {
                await _context.Enrollments.Include(e => e.Course).ToListAsync();
            }
            return results;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<Student> Update(Student obj)
        {
            try
            {
                var update = await _context.Students.Include(s => s.Enrollments).FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (update == null) throw new($"Data Tidak dengan Id {obj.ID} Tidak ditemukan");
                update.FirstMidName = obj.FirstMidName;
                update.LastName = obj.LastName;
                update.EnrollmentDate = obj.EnrollmentDate;
                await _context.SaveChangesAsync();
                return obj;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<StudentCourseDTO> UpdateFull(StudentCourseDTO obj, EnrollmentDTO enrollObj, CourseDTO courseObj)
        {
            try
            {
                var update = await _context.Students.Include(s => s.Enrollments).FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (update == null) throw new($"Data Tidak dengan Id {obj.ID} Tidak ditemukan");
                update.FirstMidName = obj.FirstMidName;
                update.LastName = obj.LastName;
                update.EnrollmentDate = obj.EnrollmentDate;
                foreach (var enroll in update.Enrollments)
                {
                    var enrollResult = await _context.Enrollments.Include(e => e.Course).FirstOrDefaultAsync(e => e.CourseID == enroll.CourseID);
                    if (enrollResult == null) throw new($"Data Tidak dengan Id {obj.ID} Tidak ditemukan");
                    enrollResult.Grade = enrollObj.Grade;
                    var course = enrollResult.Course;
                    course.Title = courseObj.Title;
                    course.Credits = courseObj.Credits;
                }
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
