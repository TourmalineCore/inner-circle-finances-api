using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class UpdateBasicSalaryParametersCommand
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class UpdateBasicSalaryParametersCommandHandler
    {
        private readonly BasicSalaryParametersRepository _basicSalaryParametersRepository;

        public UpdateBasicSalaryParametersCommandHandler(BasicSalaryParametersRepository basicSalaryParametersRepository)
        {
            _basicSalaryParametersRepository = basicSalaryParametersRepository;
        }

        public async void Handle(UpdateBasicSalaryParametersCommand request)
        {
           await _basicSalaryParametersRepository.UpdateBasicSalaryParameters(new BasicSalaryParameters(
                request.Id,
                request.EmployeeId,
                request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking));
        }
    }
}
