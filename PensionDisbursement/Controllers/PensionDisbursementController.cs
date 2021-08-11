using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PensionDisbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionDisbursementController : ControllerBase
    {
        string Baseurl = "https://localhost:44312/";
        PensionDetail pensionDetails = null;


        [HttpPost]
        public async Task<ActionResult> PostPensionDetail([FromBody] ProcessPensionInput processPensionInput)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/PensionerDetail/" + processPensionInput.AadharNumber);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    pensionDetails = JsonConvert.DeserializeObject<PensionDetail>(Response);
                }
            }
            #region Calculating Pension Amount
            
            double PensionAmount = 0;
            double BankCharges = 0;
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
            # endregion
            if ((processPensionInput.BankCharges == BankCharges) && (processPensionInput.PensionAmount == PensionAmount))
            {
                return new StatusCodeResult(10);
            }
            else
            {
                return new StatusCodeResult(21);
            }
            

            
                
               
        }
       
    }
}
