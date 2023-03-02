namespace SalaryService.Application.Dtos
{
    public class WorkingDaysDto
    {
        public decimal WorkingDaysInYear { get; set; }

        public decimal WorkingDaysInYearWithoutVacation { get; set; }

        public decimal WorkingDaysInYearWithoutVacationAndSick { get; set; }

        public decimal WorkingDaysInMonth { get; set; }

        public decimal WorkingHoursInMonth { get; set; }

        public WorkingDaysDto(decimal workingDaysInYear,
            decimal workingDaysInYearWithoutVacation,
            decimal workingDaysInYearWithoutVacationAndSick,
            decimal workingDaysInMonth,
            decimal workingHoursInMonth)
        {
            WorkingDaysInYear = workingDaysInYear;
            WorkingDaysInYearWithoutVacation = workingDaysInYearWithoutVacation;
            WorkingDaysInYearWithoutVacationAndSick = workingDaysInYearWithoutVacationAndSick;
            WorkingDaysInMonth = workingDaysInMonth;
            WorkingHoursInMonth = workingHoursInMonth;
        }
    }
}
