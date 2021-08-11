using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Process_Pension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        string Baseurl = "https://localhost:44312/";
        PensionDetail pensionDetails = null;
        ProcessPensionInput pensionInput = null;

        [HttpGet]
        public async Task<ActionResult> GetPensionDetail([FromBody]PensionerInput pensionerDetails)
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
                HttpResponseMessage Res = await client.GetAsync("api/PensionerDetail/"+pensionerDetails.AadharNo);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    pensionDetails = JsonConvert.DeserializeObject<PensionDetail>(EmpResponse);
                }
            }

            if(pensionDetails.Name == pensionerDetails.Name && pensionDetails.DateOfBirth == pensionerDetails.DateOfBirth && pensionDetails.PanNo == pensionerDetails.PanNo)
            {
                CalculatePension(pensionDetails);
                return Ok(pensionDetails);
            }
            else
                return Ok("Invalid pensioner detail provided, please provide valid detail.");
        }
        public void CalculatePension(PensionDetail pensionDetails)
        {
            if (pensionDetails.BankAccountType.Equals("Self", StringComparison.OrdinalIgnoreCase))
            {
                pensionInput.PensionAmount = (pensionDetails.SalaryEarned * 0.8) + pensionDetails.Allowance;
            }
            else if (pensionDetails.BankAccountType.Equals("family", StringComparison.OrdinalIgnoreCase))
            {
                pensionInput.PensionAmount = (pensionDetails.SalaryEarned * 0.5) + pensionDetails.Allowance;
            }
            if(pensionDetails.BankType.Equals("public",StringComparison.OrdinalIgnoreCase))
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
