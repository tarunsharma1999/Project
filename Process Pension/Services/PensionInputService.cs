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
    public class PensionInputService:IProcessPensionInput
    {
        string Baseurl2 = "https://localhost:44376/";

        public async Task<int> ProcessPensionInput(ProcessPensionInput pensionInput)
        {
            var json = JsonConvert.SerializeObject(pensionInput);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl2);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/PensionDisbursement/", data);

               return ((int)Res.StatusCode);
                
            }
        }
    }
}
