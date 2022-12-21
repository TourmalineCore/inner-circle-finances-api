namespace SalaryService.Application.Dtos
{
    public class WorkingDaysDto
    {
        public double WorkingDaysInYear { get; set; }

        public double WorkingDaysInYearWithoutVacation { get; set; }

        public double WorkingDaysInYearWithoutVacationAndSick { get; set; }

        public double WorkingDaysInMonth { get; set; }

        public double WorkingHoursInMonth { get; set; }

        public WorkingDaysDto(double workingDaysInYear, 
            double workingDaysInYearWithoutVacation, 
            double workingDaysInYearWithoutVacationAndSick, 
            double workingDaysInMonth,
            double workingHoursInMonth)
        {
            WorkingDaysInYear = workingDaysInYear;
            WorkingDaysInYearWithoutVacation = workingDaysInYearWithoutVacation;
            WorkingDaysInYearWithoutVacationAndSick = workingDaysInYearWithoutVacationAndSick;
            WorkingDaysInMonth = workingDaysInMonth;
            WorkingHoursInMonth = workingHoursInMonth;
        }
    }
}
