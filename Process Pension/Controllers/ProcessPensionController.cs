using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Process_Pension.Interfaces;
using Process_Pension.Models;
using System;
using System.Threading.Tasks;

namespace Process_Pension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessPensionController : ControllerBase
    {
        private readonly IPensionDetails pensionDetails;
        private readonly IProcessPensionInput processPensionInput;
        string JWT;

        public ProcessPensionController(IPensionDetails pensionDetails,IProcessPensionInput processPensionInput)
        {
            this.pensionDetails = pensionDetails;
            this.processPensionInput = processPensionInput;
        }

        [HttpGet]
        //public async Task<ActionResult> GetPensionDetail([FromBody]PensionerInput pensionerDetails)
        public async Task<ActionResult> GetPensionDetail(string name,DateTime dateofbirth, string panno, double aadharNo, string bankAccountType)
        {
            PensionerInput pensionerDetails = new PensionerInput
            {
                Name = name,
                DateOfBirth = dateofbirth,
                PanNo = panno,
                AadharNumber = aadharNo,
                BankAccountType = bankAccountType
            };
            var pensionInput = await pensionDetails.GetPensionDetails(pensionerDetails);
            return pensionInput != null ? Ok(pensionInput) : Ok("Invalid pensioner detail provided, please provide valid detail.");
        }

        [HttpPost]
        public async Task<ActionResult> PostProcessPension([FromBody] ProcessPensionInput pensionInput)
        {

            int statusCode = await processPensionInput.ProcessPensionInput(pensionInput);
            return statusCode == 10 ? Ok("Pension Calcucation is Correct") : Ok("Incorrect Calculcations");
        }
    }
    

}
