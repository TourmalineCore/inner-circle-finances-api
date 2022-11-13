using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class BasicSalaryParametersRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public BasicSalaryParametersRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<long> CreateAsync(BasicSalaryParameters basicSalaryParameters)
        {
            await _employeeDbContext.AddAsync(basicSalaryParameters);
            await _employeeDbContext.SaveChangesAsync();
            return basicSalaryParameters.Id;
        }

        public Task UpdateAsync(BasicSalaryParameters basicSalaryParameters)
        {
            _employeeDbContext.Update(basicSalaryParameters);
            return _employeeDbContext.SaveChangesAsync();
        }

        public Task<BasicSalaryParameters> GetByEmployeeIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<BasicSalaryParameters>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }
    }
}
