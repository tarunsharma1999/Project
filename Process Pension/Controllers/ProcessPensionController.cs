using Microsoft.AspNetCore.Mvc;
using Process_Pension.Interfaces;
using Process_Pension.Models;
using System.Threading.Tasks;

namespace Process_Pension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        private readonly IPensionDetails pensionDetails;
        private readonly IProcessPensionInput processPensionInput;

        public ProcessPensionController(IPensionDetails pensionDetails,IProcessPensionInput processPensionInput)
        {
            this.pensionDetails = pensionDetails;
            this.processPensionInput = processPensionInput;
        }

        [HttpGet]
        public async Task<ActionResult> GetPensionDetail([FromBody]PensionerInput pensionerDetails)
        {
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
