using Moq;
using NUnit.Framework;
using Process_Pension.Interfaces;
using Process_Pension.Models;
using Process_Pension.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Test_ProcessPension
{
    public class Tests
    {
        ProcessPensionInput pensionData = new ProcessPensionInput { 
                                                    AadharNumber= 111111111111, 
                                                    PensionAmount= 21300, 
                                                    BankCharges=500
                                                    };
        Mock<IHttpClientHelper> mocked_client = new Mock<IHttpClientHelper>();
        [SetUp]
        public async Task Setup()
        {
            var task = new Task<ProcessPensionInput>( () => { return pensionData; });
            var res =await Task.FromResult(pensionData);
            mocked_client.Setup(x => x.GetAsync(It.IsAny<double>())).Returns(task);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}