using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PensionManagementPortal.Interfaces
{
    public interface IClientHelper
    {
        //Task<string> GetJWT();
        Task<PensionData> SubmitPensionInput(PensionInput pensionInput);
    }
}
