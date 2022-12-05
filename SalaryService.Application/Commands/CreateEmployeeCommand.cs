using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeCommand
    {

    }
    public class CreateEmployeeCommandHandler
    {
        private readonly MailService _mailService;
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IClock _clock;

        public CreateEmployeeCommandHandler(MailService mailService,
            EmployeeDbContext employeeDbContext,
            IClock clock)
        {
            _mailService = mailService;
            _employeeDbContext = employeeDbContext;
            _clock = clock;
        }

        public Task Handle(EmployeeCreatingParameters request, EmployeeFinancialMetrics metrics)
        {
            var employee = new Employee(request.Name,
                request.Surname,
                request.MiddleName,
                request.CorporateEmail,
                request.PersonalEmail,
                request.Phone,
                request.GitHub,
                request.GitLab,
                _clock.GetCurrentInstant()); ;

            var financeForPayroll = new EmployeeFinanceForPayroll(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);

            using (var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    employee.EmployeeFinanceForPayroll = financeForPayroll;
                    employee.EmployeeFinancialMetrics = metrics;
                    financeForPayroll.Employee = employee;
                    metrics.Employee = employee;
                    _employeeDbContext.Add(employee);
                    _employeeDbContext.Add(financeForPayroll);
                    _employeeDbContext.Add(metrics);
                    transaction.Commit();

                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                }

            }
            _mailService.SendCredentials(request.PersonalEmail, request.CorporateEmail);
            return _employeeDbContext.SaveChangesAsync();
        }
    }
}
