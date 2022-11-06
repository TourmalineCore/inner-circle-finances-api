using SalaryService.Application.Dtos;
using SalaryService.DataAccess;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeSalaryParametersQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeSalaryParametersQueryHandler
    {
        private readonly FakeDatabase _fakeDataBase;

        public GetEmployeeSalaryParametersQueryHandler(FakeDatabase fakeDataBase)
        {
            _fakeDataBase = fakeDataBase;
        }
        public SalaryParametersDto Handle(GetEmployeeSalaryParametersQuery request)
        {
            var employee = _fakeDataBase.GetById(request.EmployeeId);
            return new SalaryParametersDto(
                employee.Id,
                employee.RatePerHour,
                employee.FullSalary,
                employee.EmploymentType);
        }
    }
}
