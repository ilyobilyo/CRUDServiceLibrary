using AutoMapper;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;
using TESTMyLib.Models.ResponseModels;

namespace TESTMyLib.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeInputModel, Employee>();
            CreateMap<Employee, EmployeeResponseModel>();
        }
    }
}
