using AutoMapper;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;
using TESTMyLib.Models.ResponseModels;

namespace TESTMyLib.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarInputModel, Car>();
            CreateMap<Car, CarResponseModel>();
        }
    }
}
