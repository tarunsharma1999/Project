using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Models
{
    public class ProcessPensionInput
    {
        public double AadharNumber { get; set; }

        public double PensionAmount { get; set; }

        public double BankCharges { get; set; }
    }
}
