namespace SalaryService.Domain
{
    public class WorkingPlan
    {
        public long Id { get; set; }
        public double WorkingDaysInYear { get; private set; }
        public double WorkingDaysInYearWithoutVacation { get; private set; }
        public double WorkingDaysInYearWithoutVacationAndSick { get; private set; }
        public double WorkingDaysInMonth { get; private set; }
        public double WorkingHoursInMonth { get; private set; }
        public WorkingPlan(long id,
            double workingDaysInYear, 
            double workingDaysInYearWithoutVacation, 
            double workingDaysInYearWithoutVacationAndSick, 
            double workingDaysInMonth, 
            double workingHoursInMonth)
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
