using APi.Data.Models;
using APi.Models;
using CRUDServiceLibrary.Contracts;

namespace APi.Services
{
    public interface ICarService : IBaseService<Car, CarInputModel, int>
    {
    }
}
