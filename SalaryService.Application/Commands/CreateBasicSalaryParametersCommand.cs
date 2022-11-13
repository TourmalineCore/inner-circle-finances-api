

using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateBasicSalaryParametersCommand
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class CreateBasicSalaryParametersCommandHandler
    {
        private readonly BasicSalaryParametersRepository _basicSalaryParametersRepository;

        public CreateBasicSalaryParametersCommandHandler(BasicSalaryParametersRepository basicSalaryParameters)
        {
            _basicSalaryParametersRepository = basicSalaryParameters;
        }

        public async Task<long> Handle(CreateBasicSalaryParametersCommand request)
        {
            var basicSalaryParameters = new BasicSalaryParameters(
                request.EmployeeId,
                request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);
            return await _basicSalaryParametersRepository.CreateAsync(basicSalaryParameters);
        }
    }
}
