using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Dtos
{
    public class FullEmployeeInformationDto
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string WorkEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }

        public double Pay { get; private set; }

        public double RatePerHour { get; private set; }

        public double EmploymentType { get; private set; }

        public bool HasParking { get; private set; }

        public double HourlyCostFact { get; private set; }

        public double HourlyCostHand { get; private set; }

        public double Earnings { get; private set; }

        public double Expenses { get; private set; }

        public double Profit { get; private set; }

        public double ProfitAbility { get; private set; }

        public double GrossSalary { get; private set; }

        public double Retainer { get; private set; }

        public double NetSalary { get; private set; }

        public FullEmployeeInformationDto(long id, 
            string name, 
            string surname, 
            string workEmail, 
            string personalEmail, 
            string phone, 
            string skype, 
            string telegram, 
            double pay, 
            double ratePerHour,
            double employmentType,
            bool hasParking,
            double hourlyCostFact, 
            double hourlyCostHand, 
            double earnings, 
            double expenses, 
            double profit, 
            double profitAbility, 
            double grossSalary, 
            double retainer, 
            double netSalary)
        {
            Id = id;
            Name = name;
            Surname = surname;
            WorkEmail = workEmail;
            PersonalEmail = personalEmail;
            Phone = phone;
            Skype = skype;
            Telegram = telegram;
            Pay = pay;
            RatePerHour = ratePerHour;
            EmploymentType = employmentType;
            HasParking = hasParking;
            HourlyCostFact = hourlyCostFact;
            HourlyCostHand = hourlyCostHand;
            Earnings = earnings;
            Expenses = expenses;
            Profit = profit;
            ProfitAbility = profitAbility;
            GrossSalary = grossSalary;
            Retainer = retainer;
            NetSalary = netSalary;
        }
    }
}
