using Newtonsoft.Json;
using Process_Pension.Interfaces;
using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Process_Pension.Services
{
    public class HttpClientHelper:IHttpClientHelper
    {
        private HttpClient _client;
        public HttpClientHelper()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<string> GetJWT()
        {
            //Authorization API address  https://localhost:44398/
            string Baseurl1 = "https://authorization20210819125108.azurewebsites.net/";
            _client.BaseAddress = new Uri(Baseurl1);
            var res = await _client.GetAsync("api/auth/");
            var Result = res.Content.ReadAsStringAsync().Result;
            var JWToken = JsonConvert.DeserializeObject<string>(Result);
            return JWToken;
        }

        public async Task<PensionDetail> GetAsync(double AadharNumber)
        {
            var token = await GetJWT();
            _client = new HttpClient();
            // PensionDetails Address https://localhost:44312/
            string Baseurl1 = "https://pensionerdetailproject2.azurewebsites.net";

            _client.BaseAddress = new Uri(Baseurl1);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await _client.GetAsync("api/PensionerDetail/PensionerDetailByAadhaar/" + AadharNumber);

            var Response = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PensionDetail>(Response);

        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            var token = await GetJWT();
            _client = new HttpClient();
            // Pension Disbursment https://localhost:44376/
            string Baseurl1 = "https://pensiondisbursement20210819131345.azurewebsites.net/";
            _client.BaseAddress = new Uri(Baseurl1);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _client.PostAsync(url, content);
        }
    }
}
