using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Domain.Entities;
using MentoringProject.ViewModels;

namespace MentoringProject.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OwnerCreateViewModel, OwnerDTO>();
            CreateMap<OwnerUpdateViewModel, OwnerDTO>();
            CreateMap<OwnerDTO, OwnerViewModel>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<OwnerCar, OwnerCarDTO>().ReverseMap();
            CreateMap<CarCreateViewModel, CarDTO>();
            CreateMap<CarUpdateViewModel, CarDTO>();
            CreateMap<CarDTO, CarViewModel>();
            CreateMap<OwnerCarDTO, OwnerCarViewModel>();//.ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));
        }
    }
}