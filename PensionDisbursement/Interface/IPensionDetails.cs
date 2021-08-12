using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Interface
{
    public interface IPensionDetails
    {
        Task<bool> GetPensionDetails(ProcessPensionInput pensionerDetails);
    }
}
