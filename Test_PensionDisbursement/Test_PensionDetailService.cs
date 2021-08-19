using Moq;
using NUnit.Framework;
using PensionDisbursement.Interface;
using PensionDisbursement.Models;
using PensionDisbursement.Service;
using System.Threading.Tasks;

namespace Test_PensionDisbursement
{
    public class Tests
    {
        Mock<IHttpClientHelper> mockedHttpClient = new Mock<IHttpClientHelper>();
        PensionDetailService pensionDetailService;
        ProcessPensionInput pensionInput;
        PensionDetail pensionDetails = null;

        [SetUp]
        public void Setup()
        {
            pensionDetailService = new PensionDetailService(mockedHttpClient.Object);
            pensionInput = new ProcessPensionInput { AadharNumber = 111111111111, PensionAmount = 21300, BankCharges = 500 };
        }

        [Test]
        [TestCase(123)]
        [TestCase(121233)]
        public async Task WhenUserDoesNotExist(double aadharNo)
        {
            pensionDetails = null;
            mockedHttpClient.Setup(x => x.GetAsync(aadharNo)).ReturnsAsync(pensionDetails);
            bool res = await pensionDetailService.GetPensionDetails(pensionInput);
            Assert.AreEqual(false, res);
        }

        [Test]
        public async  Task CalculatePension()
        {
            pensionDetails = new PensionDetail
            {
                Name = "User 1",
                AadharNumber = 111111111111,
                AccountNumber = 8430500021,
                Allowance = 1800,
                BankAccountType = "Self",
                BankName = "ABCD",
                BankType = "Public",
                PanNo = "ABCOS1234J",
                SalaryEarned = 25000
            };
            mockedHttpClient.Setup(x => x.GetAsync(111111111111)).ReturnsAsync(pensionDetails);
            bool res = await pensionDetailService.GetPensionDetails(pensionInput);
            pensionDetailService.CalculatePension();
            Assert.AreEqual(21300, pensionDetailService.PensionAmount);
            Assert.AreEqual(500, pensionDetailService.BankCharges);

        }

        [Test]
        public async Task PensionisCorrect()
        {
            pensionDetails = new PensionDetail
            {
                Name = "User 1",
                AadharNumber = 111111111111,
                AccountNumber = 8430500021,
                Allowance = 1800,
                BankAccountType = "Self",
                BankName = "ABCD",
                BankType = "Public",
                PanNo = "ABCOS1234J",
                SalaryEarned = 25000
            };
            mockedHttpClient.Setup(x => x.GetAsync(111111111111)).ReturnsAsync(pensionDetails);
            bool res = await pensionDetailService.GetPensionDetails(pensionInput);
            Assert.AreEqual(true, res);
        }
    }
}