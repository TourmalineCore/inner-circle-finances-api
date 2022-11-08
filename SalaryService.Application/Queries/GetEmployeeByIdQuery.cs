using SalaryService.Application.Dtos;
using SalaryService.DataAccess;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeByIdQuery
    {
        public long EmployeeId { get; set; }
    }

    public class GetEmployeeByIdQueryHandler
    {
        private readonly FakeDatabase _fakeDataBase;

        public GetEmployeeByIdQueryHandler(FakeDatabase fakeDataBase)
        {
            _fakeDataBase = fakeDataBase;
        }
        public EmployeeDto Handle(GetEmployeeByIdQuery request)
        {
            var employee = _fakeDataBase.GetById(request.EmployeeId);
            return new EmployeeDto(
                employee.Id,
                employee.Name,
                employee.Surname,
                employee.Email,
                employee.RatePerHour,
                employee.FullSalary,
                employee.EmploymentType);
        }
    }
}
