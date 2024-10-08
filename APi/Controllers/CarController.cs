using APi.Data.Models;
using APi.Models;
using APi.Services;
using CRUDServiceLibrary;
using CRUDServiceLibrary.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : CRUDController<ICarService, Car, CarInputModel, CarResponseModel, int>
    {
        public CarController(ICRUDService<ICarService, Car, CarInputModel, CarResponseModel, int> crudService) 
            : base(crudService)
        {
        }
    }
}
