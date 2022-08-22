using AutoMapper;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<CreateEnrollmentStudentDTO, Enrollment>();
            CreateMap<CreateEnrollmentDTO, Enrollment>();

            CreateMap<EnrollmentDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentDTO>();

            CreateMap<EnrollmentStudentDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentStudentDTO>();

            CreateMap<Enrollment, EnrollmentCourseStudentDTO>();
            CreateMap<EnrollmentCourseStudentDTO, Enrollment>();
        }
    }
}
