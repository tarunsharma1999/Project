using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Process_Pension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        string Baseurl1 = "https://localhost:44312/";
        string Baseurl2 = "https://localhost:44376/";

        PensionDetail pensionDetails = null;
        ProcessPensionInput pensionInput = null;

        [HttpGet]
        public async Task<ActionResult> GetPensionDetail([FromBody]PensionerInput pensionerDetails)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl1);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/PensionerDetail/"+pensionerDetails.AadharNumber);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    pensionDetails = JsonConvert.DeserializeObject<PensionDetail>(Response);
                }
            }

            #region Calcuating Pension 
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
                return Ok(pensionInput);
            }
            else
                return Ok("Invalid pensioner detail provided, please provide valid detail.");
            #endregion

        }

        [HttpPost]
        public async Task<ActionResult> PostProcessPension(ProcessPensionInput pensionInput)
        {
            var json = JsonConvert.SerializeObject(pensionInput);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl2);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsync("api/PensionDisbursement/", data);

                //Checking the response is successful or not which is sent using HttpClient
                if (( (int)Res.StatusCode) == 10)
                {
                    return Ok("Pension Calcucation is Correct");
                }
                else
                {
                    return Ok("Incorrect Calculcations");

                }
            }

        }
    }
    

}
