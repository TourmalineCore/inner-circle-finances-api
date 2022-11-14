using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateBasicSalaryParametersCommand
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class UpdateBasicSalaryParametersCommandHandler
    {
        private readonly BasicSalaryParametersRepository _basicSalaryParametersRepository;

        public UpdateBasicSalaryParametersCommandHandler(BasicSalaryParametersRepository basicSalaryParametersRepository)
        {
            _basicSalaryParametersRepository = basicSalaryParametersRepository;
        }

        public async Task Handle(UpdateBasicSalaryParametersCommand request)
        {
            var basicParameters = _basicSalaryParametersRepository.GetByEmployeeIdAsync(request.EmployeeId).Result;
            basicParameters.Update(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);

           await _basicSalaryParametersRepository.UpdateAsync(basicParameters);
        }
    }
}
