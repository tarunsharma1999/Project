using Microsoft.AspNetCore.Mvc;
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
    public class PensionDetails:IPensionDetails
    {
        string Baseurl1 = "https://localhost:44312/";
        PensionDetail pensionDetails;
        ProcessPensionInput pensionInput = null;


        public async Task<ProcessPensionInput> GetPensionDetails(PensionerInput pensionerDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl1);
                client.DefaultRequestHeaders.Clear();

                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/PensionerDetail/" + pensionerDetails.AadharNumber);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    pensionDetails = JsonConvert.DeserializeObject<PensionDetail>(Response);
                }
            }
            CalculatePension(pensionerDetails);
            return pensionInput;
        }
        public void CalculatePension(PensionerInput pensionerDetails)
        {
            if (pensionDetails.Name == pensionerDetails.Name && pensionDetails.DateOfBirth == pensionerDetails.DateOfBirth && pensionDetails.PanNo == pensionerDetails.PanNo)
            {
                pensionInput = new ProcessPensionInput();
                pensionInput.AadharNumber = pensionDetails.AadharNumber;
                if (pensionDetails.BankAccountType.Equals("Self", StringComparison.OrdinalIgnoreCase))
                {
                    pensionInput.PensionAmount = (pensionDetails.SalaryEarned * 0.8) + pensionDetails.Allowance;
                }
                else if (pensionDetails.BankAccountType.Equals("family", StringComparison.OrdinalIgnoreCase))
                {
                    pensionInput.PensionAmount = (pensionDetails.SalaryEarned * 0.5) + pensionDetails.Allowance;
                }
                if (pensionDetails.BankType.Equals("public", StringComparison.OrdinalIgnoreCase))
                {
                    pensionInput.PensionAmount -= 500;
                    pensionInput.BankCharges = 500;
                }
                else if (pensionDetails.BankType.Equals("private", StringComparison.OrdinalIgnoreCase))
                {
                    pensionInput.PensionAmount -= 550;
                    pensionInput.BankCharges = 550;
                }
              
            }
        }
    }
}

