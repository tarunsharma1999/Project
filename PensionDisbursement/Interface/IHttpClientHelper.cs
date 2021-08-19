using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Interface
{
    public interface IHttpClientHelper
    {
        Task<string> GetJWT();
        Task<PensionDetail> GetAsync(double AadharNumber);
    }
}
