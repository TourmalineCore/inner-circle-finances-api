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

        public async Task<long> CreateBasicSalaryParameters(BasicSalaryParameters basicSalaryParameters)
        {
            await _employeeDbContext.AddAsync(basicSalaryParameters);
            await _employeeDbContext.SaveChangesAsync();
            return basicSalaryParameters.EmployeeId;
        }

        public async Task UpdateBasicSalaryParameters(BasicSalaryParameters basicSalaryParameters)
        {
            _employeeDbContext.Update(basicSalaryParameters);
            await _employeeDbContext.SaveChangesAsync();
        }

        public Task<BasicSalaryParameters> GetBasicSalaryParametersByEmployeeIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<BasicSalaryParameters>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }
    }
}
