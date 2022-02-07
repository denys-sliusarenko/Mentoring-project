﻿using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Domain.Entities;
using MentoringProject.ViewModels;

namespace MentoringProject.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOwnerViewModel, OwnerDTO>();
            CreateMap<UpdateOwnerViewModel, OwnerDTO>();
            CreateMap<OwnerDTO, OwnerViewModel>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<OwnerCar, OwnerCarDTO>().ReverseMap();
        }
    }
}