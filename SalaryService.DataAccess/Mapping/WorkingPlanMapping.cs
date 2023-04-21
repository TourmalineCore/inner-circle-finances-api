using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping;

public class WorkingPlanMapping : IEntityTypeConfiguration<WorkingPlan>
{
    public void Configure(EntityTypeBuilder<WorkingPlan> builder)
    {
        decimal workingDaysInYear = 247;
        decimal workingDaysInYearWithoutVacation = workingDaysInYear - 24;
        decimal workingDaysInYearWithoutVacationAndSick = workingDaysInYearWithoutVacation - 20;
        decimal workingDaysInMonth = workingDaysInYearWithoutVacationAndSick / 12;
        decimal workingHoursInMonth = workingDaysInMonth * 8;

        builder.HasData(new WorkingPlan(1L,
            workingDaysInYear,
            workingDaysInYearWithoutVacation,
            workingDaysInYearWithoutVacationAndSick,
            workingDaysInMonth,
            workingHoursInMonth)
        );
    }
}
