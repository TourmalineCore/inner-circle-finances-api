namespace SalaryService.Domain
{
    public class WorkingPlan
    {
        public long Id { get; set; }

        public decimal WorkingDaysInYear { get; private set; }

        public decimal WorkingDaysInYearWithoutVacation { get; private set; }

        public decimal WorkingDaysInYearWithoutVacationAndSick { get; private set; }

        public decimal WorkingDaysInMonth { get; private set; }

        public decimal WorkingHoursInMonth { get; private set; }

        public WorkingPlan(long id,
            decimal workingDaysInYear,
            decimal workingDaysInYearWithoutVacation,
            decimal workingDaysInYearWithoutVacationAndSick,
            decimal workingDaysInMonth,
            decimal workingHoursInMonth)
        {
            Id = id;
            WorkingDaysInYear = workingDaysInYear;
            WorkingDaysInYearWithoutVacation = workingDaysInYearWithoutVacation;
            WorkingDaysInYearWithoutVacationAndSick = workingDaysInYearWithoutVacationAndSick;
            WorkingDaysInMonth = workingDaysInMonth;
            WorkingHoursInMonth = workingHoursInMonth;
        }
    }
}
