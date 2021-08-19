using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionDisbursement.Interface;
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
    [Authorize]
    public class PensionDisbursementController : ControllerBase
    {
        private readonly IPensionDetails pensionDetails;
       

        public PensionDisbursementController(IPensionDetails pensionDetails)
        {
            this.pensionDetails = pensionDetails;
        }


        [HttpPost]
        public async Task<ActionResult> PostPensionDetail([FromBody] ProcessPensionInput processPensionInput)
        {
            var result = await pensionDetails.GetPensionDetails(processPensionInput);
            
            return result ? new StatusCodeResult(10) : new StatusCodeResult(21); 
        }
       
    }
}
