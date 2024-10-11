using Microsoft.AspNetCore.Mvc;
using TESTMyLib.Contracts;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;
using TESTMyLib.Models.ResponseModels;

namespace TESTMyLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : CRUDServiceLibrary.CRUDController<ICarService, Car, CarInputModel, CarResponseModel, int>
    {
        public CarController(CRUDServiceLibrary.Contracts.ICRUDService<ICarService, Car, CarInputModel, CarResponseModel, int> crudService) : base(crudService)
        {
        }
    }
}
