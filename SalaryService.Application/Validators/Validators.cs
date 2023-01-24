using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Validators
{
    internal static class Validators
    {
        public static bool CompareEmployeeTypeValue(double value)
        {
            return new List<double>() { 1, 0.5 }.Contains(value);
        }
    }
}
