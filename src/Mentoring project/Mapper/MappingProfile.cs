using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.ViewModels;

namespace MentoringProject.Mapper
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
