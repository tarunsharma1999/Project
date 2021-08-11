using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Process_Pension.Models
{
    public class PensionDetail
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PanNo { get; set; }
        public string BankAccountType { get; set; }
        public double SalaryEarned { get; set; }

        public double Allowance { get; set; }
        public string BankType { get; set; }


        public double PensionAmount { get; set; }
    }
}
