using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tugas2WebAPI.DTO;
using Tugas2WebAPI.Models;

namespace Tugas2WebAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateRoleDTO, CustomRole>();

            CreateMap<RoleDTO, CustomRole>();
            CreateMap<CustomRole, RoleDTO>();

            CreateMap<UserRole, UserRoleGateDTO>();
            CreateMap<UserRoleGateDTO, UserRole>();

            CreateMap<CustomIdentityUser, UserRoleListDTO>();
            CreateMap<UserRoleListDTO, CustomIdentityUser>();

            CreateMap<ReadUserDTO, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, ReadUserDTO>();
        }
    }
}
