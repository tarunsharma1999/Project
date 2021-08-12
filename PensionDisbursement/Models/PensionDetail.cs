﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Models
{
    public class PensionDetail
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PanNo { get; set; }

        public double AadharNumber { get; set; }

        public double SalaryEarned { get; set; }

        public double Allowance { get; set; }
        public string BankAccountType { get; set; }

        public string BankName { get; set; }

        public double AccountNumber { get; set; }
        public string BankType { get; set; }
    }
}