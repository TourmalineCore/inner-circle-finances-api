//using NodaTime;
//using SalaryService.Application.Dtos;
//using SalaryService.Domain;

//namespace SalaryService.Api.Responses;

//public class CompensationResponse
//{
//    public List<RowCompensationDto> Rows { get; init; }

//    public double TotalUnpaidAmount { get; init; }

//    public CompensationResponse(List<CompensationDto> compensation, double totalAmount)
//    {
//        TotalUnpaidAmount = totalAmount;
//        // TotalUnpaidAmount = Math.Round(totalAmount, 2);

//        Rows = compensation.Select(x => new RowCompensationDto
//        {
//            if (comment != null)
//            Comment = x.Comment,
//            Amount = x.Amount,
//            IsPaid = x.IsPaid,
//            CreatedAtUtc = x.
//            Date = x.
//            //date
//            //bool
//        });


//        if (!isPaid) return;

//        //HireDate = employee.HireDate?.ToDateTimeUtc();
//    }
//}

//public class RowCompensationDto
//{
//    //? public long Id { get; init; }

//    public string? Comment { get; init; }

//    public double Amount { get; init; }

//    public bool IsPaid { get; init; }

//    public Instant CreatedAtUtc { get; init; }

//    public DateOnly Date { get; init; }
//}