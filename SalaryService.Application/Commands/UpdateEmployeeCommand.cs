using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeCommand
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }
    }
    public class UpdateEmployeeCommandHandler
    {
        private readonly FakeDatabase _fakeDataBase;

        public UpdateEmployeeCommandHandler(FakeDatabase fakeDataBase)
        {
            _fakeDataBase = fakeDataBase;
        }
        public void Handle(UpdateEmployeeCommand request)
        {
            _fakeDataBase.UpdateAsync(request.Id, new Employee(
                request.Id,
                request.Name,
                request.Surname,
                request.Email,
                request.RatePerHour,
                request.FullSalary,
                request.EmploymentType));
        }
    }
}
