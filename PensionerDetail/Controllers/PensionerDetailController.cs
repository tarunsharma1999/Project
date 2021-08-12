using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionerDetail.Interface;
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
        private readonly ICsvManager csvManager;
        private readonly IUserDetails userDetails;

        public PensionerDetailController(ICsvManager csvManager, IUserDetails userDetails)
        {
            this.csvManager = csvManager;
            this.userDetails = userDetails;
        }

        [HttpGet("{aadharNumber:double}")]
        public ActionResult PensionerDetailByAadhaar(double aadharNumber)
        {
            var csvData = csvManager.loadData();
            var validUser = userDetails.GetuserDetails(csvData, aadharNumber);
            return Ok(validUser);
            
        }
    }
}
