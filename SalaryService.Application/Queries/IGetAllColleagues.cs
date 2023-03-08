using SalaryService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Queries
{
    public interface IGetAllColleagues
    {
        public Task<IEnumerable<CollegueInfoDto>> HandleAsync();
    }
}
