using AutoMapper;
using Mentoring_project.Business.DTO;
using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.ViewModels;

namespace Mentoring_project.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserViewModel, UserDTO>();
            CreateMap<UpdateUserViewModel, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
