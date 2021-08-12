using PensionerDetail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Services
{
    public class UserDetails:IUserDetails
    {

        public PensionerDetailModel GetuserDetails(List<PensionerDetailModel> pernsionerDetails, double aadharNumber)
        {
            PensionerDetailModel validUser = null;

            foreach (var detail in pernsionerDetails)
            {
                if (detail.AadharNumber == aadharNumber)
                {
                    validUser = detail;
                }
            }
            return validUser;
        }
    }
}
