
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeFinanceForPayrollCommand
    {
        public long Id { get; set; }
    }
    public class DeleteEmployeeFinanceForPayrollCommandHandler
    {
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;

        public DeleteEmployeeFinanceForPayrollCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
        }

        public async Task Handle(DeleteEmployeeFinanceForPayrollCommand request)
        {
            var financeForPayroll = await _employeeFinanceForPayrollRepository.GetByIdAsync(request.Id);

            await _employeeFinanceForPayrollRepository.RemoveAsync(financeForPayroll);
        }
    }
}
