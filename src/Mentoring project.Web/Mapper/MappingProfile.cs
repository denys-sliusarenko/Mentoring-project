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
            // Owner service
            CreateMap<OwnerCreateViewModel, OwnerDTO>();
            CreateMap<OwnerUpdateViewModel, OwnerDTO>();
            CreateMap<OwnerDTO, OwnerViewModel>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();

            // Car service
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<CarCreateViewModel, CarDTO>();
            CreateMap<CarUpdateViewModel, CarDTO>();
            CreateMap<CarDTO, CarViewModel>();

            // Owner car service
            CreateMap<OwnerCar, OwnerCarDTO>().ReverseMap();
            CreateMap<OwnerCarCreatedViewModel, OwnerCarDTO>().ReverseMap();
            CreateMap<OwnerCarDTO, OwnerCarViewModel>();
        }
    }
}