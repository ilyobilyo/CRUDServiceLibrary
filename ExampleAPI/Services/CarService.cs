using Microsoft.EntityFrameworkCore;
using TESTMyLib.Contracts;
using TESTMyLib.Data;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;

namespace TESTMyLib.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _appDbContext;

        public CarService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Car>> GetAll(CancellationToken cancellationToken)
        {
            return await _appDbContext.Cars
                .ToListAsync(cancellationToken);
        }

        public async Task<Car> GetById(int id, CancellationToken cancellationToken)
        {
            var car = await _appDbContext.Cars
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (car is null)
            {
                throw new ArgumentException("Car not found!");
            }

            return car;
        }

        public async Task<Car> Create(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(dataEntity,cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<Car> Update(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            dataEntity.Brand = inputModel.Brand;
            dataEntity.Color = inputModel.Color;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<int> Delete(Car dataEntity, CancellationToken cancellationToken)
        {
            _appDbContext.Remove(dataEntity);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity.Id;
        }
    }
}
