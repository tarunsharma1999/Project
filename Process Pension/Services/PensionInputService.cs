using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Process_Pension.Interfaces;
using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Process_Pension.Services
{
    public class PensionInputService : IProcessPensionInput
    {
        private readonly IHttpClientHelper _client;

        public PensionInputService(IHttpClientHelper httpClientHelper)
        {
            _client = httpClientHelper;
        }

        public async Task<int> ProcessPensionInput(ProcessPensionInput pensionInput)
        {
            var json = JsonConvert.SerializeObject(pensionInput);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var Res = await _client.PostAsync("api/PensionDisbursement/", data);
            return ((int)Res.StatusCode);
        }
    }
}
