using CRUDServiceLibrary;
using CRUDServiceLibrary.Contracts;
using Microsoft.AspNetCore.Mvc;
using TESTMyLib.Contracts;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;
using TESTMyLib.Models.ResponseModels;

namespace TESTMyLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : CRUDController<ICarService, Car, CarInputModel, CarResponseModel, int>
    {
        public CarController(ICRUDService<ICarService, Car, CarInputModel, CarResponseModel, int> crudService) : base(crudService)
        {
        }
    }
}
