using Newtonsoft.Json;
using PensionManagementPortal.Interfaces;
using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PensionManagementPortal.Services
{
    public class ClientHelperService:IClientHelper
    {
        private HttpClient _client = new HttpClient();
        public ClientHelperService()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }
        public async Task<string> GetJWT()
        {
            //https://localhost:44398/
            string address = "https://authorization20210819125108.azurewebsites.net/";
            string token= string.Empty;
            _client.BaseAddress = new Uri(address);
            var res = await _client.GetAsync("api/auth/");
            if (res.IsSuccessStatusCode)
            {
                var Response = res.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<string>(Response);
            }
            return token;
        }
        
        public async Task<PensionData> SubmitPensionInput(PensionInput pensionInput)
        {
            var token = await GetJWT();
            _client = new HttpClient();
            //https://localhost:44340/
            string address = "https://processpension20210819131250.azurewebsites.net/";
            _client.BaseAddress = new Uri(address);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await _client.GetAsync("api/processpension/?name=" + pensionInput.Name +"&dateofbirth="+pensionInput.DateOfBirth + "&panno="+ pensionInput.PanNo + "&aadharNo=" + pensionInput.AadharNumber + "&bankAccountType=" + pensionInput.BankAccountType);

            var Response = res.Content.ReadAsStringAsync().Result;
            try
            {
                return JsonConvert.DeserializeObject<PensionData>(Response);
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
