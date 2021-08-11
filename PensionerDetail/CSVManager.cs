using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;

namespace PensionerDetail
{
    public class CSVManager
    {
        List<PensionerDetailModel> pernsionerDetails = new List<PensionerDetailModel>();
        

        public List<PensionerDetailModel> loadData()
        {

            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(@"..\Pensioner Details.csv")), true))
            {
                csvTable.Load(csvReader);
            }
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                pernsionerDetails.Add(new PensionerDetailModel 
                                    {
                                        Name = csvTable.Rows[i][1].ToString().Trim(),
                                        DateOfBirth = DateTime.ParseExact(csvTable.Rows[i][2].ToString().Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture),
                                        PanNo = csvTable.Rows[i][3].ToString().Trim(),
                                        AadharNumber = Convert.ToDouble(csvTable.Rows[i][4].ToString().Trim().Replace(" ", "")),
                                        SalaryEarned = Convert.ToDouble(csvTable.Rows[i][5].ToString().Trim()),
                                        Allowance = Convert.ToDouble(csvTable.Rows[i][6].ToString().Trim()),
                                        BankAccountType = csvTable.Rows[i][7].ToString().Trim(),
                                        BankName = csvTable.Rows[i][8].ToString().Trim(),
                                        AccountNumber = Convert.ToDouble(csvTable.Rows[i][9].ToString().Trim()),
                                        BankType = csvTable.Rows[i][10].ToString().Trim()

                                    }) ;
            }
            return pernsionerDetails;
        }
    }
}
