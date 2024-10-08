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

        public async Task<IEnumerable<Employee>> GetAll(CancellationToken cancellationToken)
        {
            return await appDbContext.Employees
                .Include(x => x.Car)
                .ToListAsync(cancellationToken);
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
    }
}
