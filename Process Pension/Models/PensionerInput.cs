using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Process_Pension.Models
{
    public class PensionerInput
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PanNo { get; set; }

        public double AadharNumber { get; set; }
        public string BankAccountType { get; set; }

    }
}
