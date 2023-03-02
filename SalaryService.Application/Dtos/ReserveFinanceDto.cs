namespace SalaryService.Application.Dtos
{
    public class ReserveFinanceDto
    {
        public decimal ReserveForQuarter { get; private set; }
        public decimal ReserveForHalfYear { get; private set; }
        public decimal ReserveForYear { get; private set; }

        public ReserveFinanceDto(decimal reserveForQuarter, decimal reserveForHalfYear, decimal reserveForYear)
        {
            ReserveForQuarter = reserveForQuarter;
            ReserveForHalfYear = reserveForHalfYear;
            ReserveForYear = reserveForYear;
        }
    }
}
