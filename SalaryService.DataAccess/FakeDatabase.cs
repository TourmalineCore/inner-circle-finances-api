using SalaryService.Domain;

namespace SalaryService.DataAccess
{
    public class FakeDatabase
    {
        private readonly Dictionary<long, Employee> _data = new Dictionary<long, Employee>();

        public void SaveAsync(Employee employee)
        {
            _data.Add(employee.Id, employee);
        }

        public void UpdateAsync(long employeeId, Employee employee)
        {
            _data[employeeId] = employee;
        }

        public Employee? GetById(long empId)
        {
            _data.TryGetValue(empId, out var employee);

            return employee;
        }
    }
}
