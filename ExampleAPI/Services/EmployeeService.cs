using Microsoft.EntityFrameworkCore;
using TESTMyLib.Contracts;
using TESTMyLib.Data;
using TESTMyLib.Data.Models;
using TESTMyLib.Models.InputModels;

namespace TESTMyLib.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext appDbContext;

        public EmployeeService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> GetById(int id, CancellationToken cancellationToken)
        {
            var employee = await appDbContext.Employees
                .Include(x => x.Car)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (employee is null)
            {
                throw new ArgumentException("Employee not found!");
            }

            return employee;
        }

        public async Task<Employee> Create(Employee dataEntity, EmployeeInputModel inputModel, CancellationToken cancellationToken)
        {
            await appDbContext.AddAsync(dataEntity, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<Employee> Update(Employee dataEntity, EmployeeInputModel inputModel, CancellationToken cancellationToken)
        {
            dataEntity.Name = inputModel.Name;
            dataEntity.Age = inputModel.Age;
            dataEntity.Town = inputModel.Town;
            dataEntity.Salary = inputModel.Salary;
            dataEntity.CarId = inputModel.CarId;

            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity;
        }

        public async Task<int> Delete(Employee dataEntity, CancellationToken cancellationToken)
        {
            appDbContext.Remove(dataEntity);

            await appDbContext.SaveChangesAsync(cancellationToken);

            return dataEntity.Id;
        }

        public async Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken, Dictionary<string, string> filters = null, int? take = null, int? pageNumber = null)
        {
            IQueryable<Employee> query = appDbContext.Employees
    .Include(x => x.Car);

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    var propertyName = filter.Key;
                    var filterValue = filter.Value;

                    // Check if the property exists in the Employee model
                    var propertyInfo = typeof(Employee).GetProperty(propertyName);
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
