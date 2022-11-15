
namespace SalaryService.Domain.Common
{
    internal class WorkingPlanConsts
    {
        public const double WorkingDaysInYear = 247;

        public const double WorkingDaysInYearWithoutVacation = WorkingDaysInYear - 24;

        public const double WorkingDaysInYearWithoutVacationAndSick = WorkingDaysInYearWithoutVacation - 20;

        public const double WorkingDaysInMonth = WorkingDaysInYearWithoutVacationAndSick / 12;

        public const double WorkingHoursInMonth = WorkingDaysInMonth * 8;
    }
}
