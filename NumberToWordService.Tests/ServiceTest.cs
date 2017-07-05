using NUnit.Framework;
using System;
using System.ServiceModel;

namespace NumberToWordService.Tests
{
    [TestFixture]
    public class ServiceTest
    {
        private NumberToWordService.ServiceClient client;

        [OneTimeSetUp]
        public void Initialize()
        {
            this.client = new NumberToWordService.ServiceClient();
            this.client.Open();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (this.client.State != CommunicationState.Closed)
                this.client.Close();
        }

        //Some times this test method gives the end point error. Please try rebuild and re-run ot execute this in debug mode to test the WCF.
        [Test]
        public void ServiceCallTest_ConvertToWords()
        {
            // var res= this.client.ConvertToWords(1023.56);
            var res = new NumberToWordService.ServiceClient().ConvertToWords(23);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Contains("THREE"));

        }
    }
}
