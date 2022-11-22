
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinanceForPayrollCommand
    {
        
    }
    public class DeleteEmployeeFinanceForPayrollCommandHandler
    {
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;

        public DeleteEmployeeFinanceForPayrollCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
        }

        public async Task Handle(long employeeId)
        {
            var financeForPayroll = await _employeeFinanceForPayrollRepository.GetByEmployeeIdAsync(employeeId);

            await _employeeFinanceForPayrollRepository.RemoveAsync(financeForPayroll);
        }
    }
}
