using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Process_Pension.Interfaces
{
    
    public interface IHttpClientHelper
    {
        //Task<string> GetJWT();
        Task<PensionDetail> GetAsync(double AadharNumber);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}
