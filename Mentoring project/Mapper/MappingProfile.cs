using AutoMapper;
using Mentoring_project.DTO;
using Mentoring_project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentoring_project.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
