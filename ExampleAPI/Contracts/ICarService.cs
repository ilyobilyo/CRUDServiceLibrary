
using CRUDServiceLibrary.Contracts;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;

namespace TESTMyLib.Contracts
{
    public interface ICarService : IBaseService<Car, CarInputModel, int>
    {
    }
}
