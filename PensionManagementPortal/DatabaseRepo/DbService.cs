using PensionManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortal.DatabaseRepo
{
    public class DbService:IDb
    {
        private readonly DbHelper dbHelper;

        public DbService(DbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public PensionInput AddUserDetails(PensionInput userDetails)
        {
            try
            {
                dbHelper.userDetails.Add(userDetails);
                dbHelper.SaveChanges();
                return userDetails;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public void AddPensionDetails(PensionData pensionData)
        {
            try
            {
                dbHelper.pension.Add(pensionData);
                dbHelper.SaveChanges();
            }
            catch(Exception e)
            {
                
            }
        }

        public bool CheckPensionWithdrawn(double aadharNumber)
        {
            try
            {
                var d = dbHelper.userDetails.First(x => x.AadharNumber == aadharNumber);
                if (d.AadharNumber == aadharNumber)
                {
                    return true;
                }
            }
            catch(Exception e)
            {
                
            }
            return false;

        }

        public DateTime DateWithdrawn(double aadharNumber)
        {
            var d = dbHelper.pension.First(x => x.AadharNumber == aadharNumber);
            if (d.AadharNumber == aadharNumber)
            {
                return d.DateOFWithdraw;
            }
            return default;
        }
    }
}
