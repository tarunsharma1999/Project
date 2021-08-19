using Newtonsoft.Json;
using PensionDisbursement.Interface;
using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PensionDisbursement.Service
{
    public class PensionDetailService: IPensionDetails
    {
        string Baseurl1 = "https://localhost:44312/";
        PensionDetail pensionDetails = null;
        private readonly IHttpClientHelper client;

        public PensionDetailService(IHttpClientHelper client)
        {
            this.client = client;
        }
        public double PensionAmount { get; set; }

        public double BankCharges { get; set; } 


        public async Task<bool> GetPensionDetails(ProcessPensionInput pensionerDetails)
        {
            pensionDetails = await client.GetAsync(pensionerDetails.AadharNumber);
            
            if(pensionDetails == null)
            {
                return false;
            }
            CalculatePension();
            if(PensionAmount == pensionerDetails.PensionAmount && BankCharges == pensionerDetails.BankCharges)
            {
                return true;
            }
            else
                return false;
        }
        public void CalculatePension()
        {
            
            if (pensionDetails.BankAccountType.Equals("Self", StringComparison.OrdinalIgnoreCase))
            {
                PensionAmount = (pensionDetails.SalaryEarned * 0.8) + pensionDetails.Allowance;
            }
            else if (pensionDetails.BankAccountType.Equals("family", StringComparison.OrdinalIgnoreCase))
            {
                PensionAmount = (pensionDetails.SalaryEarned * 0.5) + pensionDetails.Allowance;
            }
            if (pensionDetails.BankType.Equals("public", StringComparison.OrdinalIgnoreCase))
            {
                PensionAmount -= 500;
                BankCharges = 500;
            }
            else if (pensionDetails.BankType.Equals("private", StringComparison.OrdinalIgnoreCase))
            {
                PensionAmount -= 550;
                BankCharges = 550;
            }
        }
    }
}
