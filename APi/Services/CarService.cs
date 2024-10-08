using APi.Data;
using APi.Data.Models;
using APi.Models;
using Microsoft.EntityFrameworkCore;

namespace APi.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext appDbContext;

        public CarService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Car> Create(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            await appDbContext.AddAsync(dataEntity, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public Task<int> Delete(Car dataEntity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetAll(CancellationToken cancellationToken)
        {
            return await appDbContext.Cars
                .ToListAsync(cancellationToken);
        }

        public Task<Car> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Car> Update(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
