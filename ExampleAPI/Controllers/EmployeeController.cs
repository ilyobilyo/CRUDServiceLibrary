﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TESTMyLib.Contracts;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;
using TESTMyLib.Models.ResponseModels;

namespace TESTMyLib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : CRUDServiceLibrary.CRUDController<IEmployeeService, Employee, EmployeeInputModel, EmployeeResponseModel, int>
    {
        public EmployeeController(CRUDServiceLibrary.Contracts.ICRUDService<IEmployeeService, Employee, EmployeeInputModel, EmployeeResponseModel, int> crudService) : base(crudService)
        {
        }
    }
}
