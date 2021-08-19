using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortal.DatabaseRepo
{
    public interface IDb
    {
        PensionInput AddUserDetails(PensionInput userDetails);
        void AddPensionDetails(PensionData pensionData);
        bool CheckPensionWithdrawn(double aadharNumber);
        DateTime DateWithdrawn(double aadharNumber);
    }
}
