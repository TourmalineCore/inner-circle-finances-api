using NodaTime;
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
        private readonly IClock _clock;

        public DeleteEmployeeFinanceForPayrollCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository, IClock clock)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteEmployeeFinanceForPayrollCommand request)
        {
            var financeForPayroll = await _employeeFinanceForPayrollRepository.GetByIdAsync(request.Id);

            financeForPayroll.Delete(_clock.GetCurrentInstant());

            await _employeeFinanceForPayrollRepository.UpdateAsync(financeForPayroll);
        }
    }
}
