using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        CSVManager csvManager = new CSVManager();

        [HttpGet("{aadharNumber:double}")]
        public ActionResult PensionerDetailByAadhaar(double aadharNumber)
        {
            var userDetails = csvManager.loadData();
            PensionerDetailModel validUser=null;
            foreach (var detail in userDetails)
            {
                if(detail.AadharNumber == aadharNumber)
                {
                    validUser = detail;
                }
            }
            return Ok(validUser);
            
        }
    }
}
