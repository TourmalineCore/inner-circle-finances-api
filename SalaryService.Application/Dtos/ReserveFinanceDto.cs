namespace SalaryService.Application.Dtos
{
    public class ReserveFinanceDto
    {
        public double ReserveForQuarter { get; private set; }
        public double ReserveForHalfYear { get; private set; }
        public double ReserveForYear { get; private set; }

        public ReserveFinanceDto(double reserveForQuarter, double reserveForHalfYear, double reserveForYear)
        {
            ReserveForQuarter = reserveForQuarter;
            ReserveForHalfYear = reserveForHalfYear;
            ReserveForYear = reserveForYear;
        }
    }
}
