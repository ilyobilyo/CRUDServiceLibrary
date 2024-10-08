using CRUDServiceLibrary.Contracts;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;

namespace TESTMyLib.Contracts
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeInputModel, int>
    {
    }
}
