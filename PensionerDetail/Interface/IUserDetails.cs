using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Interface
{
    public interface IUserDetails
    {
        PensionerDetailModel GetuserDetails(List<PensionerDetailModel> pernsionerDetails, double aadharNumber);
    }
}
