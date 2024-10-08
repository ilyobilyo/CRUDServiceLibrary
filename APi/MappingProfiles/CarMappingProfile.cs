using APi.Data.Models;
using APi.Models;
using AutoMapper;

namespace APi.MappingProfiles
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
