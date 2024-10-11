using Microsoft.EntityFrameworkCore;
using System;
using TESTMyLib.Contracts;
using TESTMyLib.Data;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;

namespace TESTMyLib.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext appDbContext;

        public CarService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<Car> GetById(int id, CancellationToken cancellationToken)
        {
            var car = await appDbContext.Cars
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (car is null)
            {
                throw new ArgumentException("Car not found!");
            }

            return car;
        }

        public async Task<Car> Create(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            await appDbContext.AddAsync(dataEntity,cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<Car> Update(Car dataEntity, CarInputModel inputModel, CancellationToken cancellationToken)
        {
            dataEntity.Brand = inputModel.Brand;
            dataEntity.Color = inputModel.Color;

            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<int> Delete(Car dataEntity, CancellationToken cancellationToken)
        {
            appDbContext.Remove(dataEntity);

            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity.Id;
        }

        public async Task<IEnumerable<Car>> GetAll(CancellationToken cancellationToken, Dictionary<string, string> filters = null, int? take = null, int? pageNumber = null)
        {
            IQueryable<Car> query = appDbContext.Cars;

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    var propertyName = filter.Key;
                    var filterValue = filter.Value;

                    // Check if the property exists in the Employee model
                    var propertyInfo = typeof(Car).GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        // Build the query dynamically based on the property type
                        query = query.Where(e => EF.Property<string>(e, propertyName) == filterValue);
                    }
                }
            }

            if (pageNumber > 0 && take is not null)
            {
                int skip = (pageNumber.Value - 1) * take.Value;
                query = query
                    .Skip(skip)
                    .Take(take.Value);
            }
            else if (take is not null)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
