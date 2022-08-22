using AutoMapper;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseDTO, Course>();

            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();

            CreateMap<Course, CourseStudentDTO>();
            CreateMap<CourseStudentDTO, Course>();
        }
    }
}
