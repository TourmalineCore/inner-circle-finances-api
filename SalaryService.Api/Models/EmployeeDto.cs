using System;
using SalaryService.Domain;

namespace SalaryService.Api.Models;

public class EmployeeDto
{
    public long Id { get; }

    public string FullName { get; }

    public string CorporateEmail { get; }

    public EmployeeDto(Employee employee)
    {
        Id = employee.Id;
        FullName = employee.GetFullName();
        CorporateEmail = employee.CorporateEmail;
    }
}

