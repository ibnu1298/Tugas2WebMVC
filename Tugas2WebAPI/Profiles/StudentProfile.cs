using AutoMapper;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<CreateStudentDTO, Student>();

            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();

            CreateMap<Student, StudentCourseDTO>();
            CreateMap<StudentCourseDTO, Student>();

            CreateMap<CreateStudentCourseDTO, Student>();
        }
    }
}
