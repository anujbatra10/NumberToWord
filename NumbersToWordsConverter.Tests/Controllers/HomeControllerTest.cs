using NumbersToWordsConverter.Models;
using NumbersToWordsConverter.NumberToWordService;
using NUnit.Framework;
using Rhino.Mocks;
using System.Web.Mvc;

namespace NumbersToWordsConverter.Controllers.Tests
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController controllerInstance;
        IService mock;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            mock = MockRepository.GenerateMock<IService>();
            controllerInstance = new HomeController(mock);
        }

        [Test]
        public void IndexGetTest()
        {
            var result = controllerInstance.Index();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);

        }

        [Test]
        public void IndexPostTest()
        {

            mock.Expect(t => t.ConvertToWords(22)).Return("Twenty Two");
            var viewResult = controllerInstance.Index(GetTesttUserModel()) as ViewResult;
            string result = viewResult.ViewBag.Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("Anuj"));
            mock.VerifyAllExpectations();
        }


        [Test]
        public void IndexPostWithInvalidModel()
        {

            controllerInstance.ViewData.ModelState.AddModelError("Key", "ErrorMessage");
            var viewResult = controllerInstance.Index(GetTesttUserModel()) as ViewResult;
            string result = viewResult.ViewBag.Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("Please enter the correct details."));

        }

        [Test]
        public void IndexPostWithException()
        {
            IService mock = MockRepository.GenerateMock<IService>();
            controllerInstance = new HomeController(mock);
            mock.Expect(t => t.ConvertToWords(22)).Throw(new System.Exception());
            var viewResult = controllerInstance.Index(GetTesttUserModel()) as ViewResult;
            string result = viewResult.ViewBag.Result;
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Contains("Anuj"));
            Assert.IsTrue(result.Contains("Error"));
            mock.VerifyAllExpectations();


        }

        private UserModel GetTesttUserModel()
        {
            return new UserModel() { Name = "Anuj test", Number = 22 };

        }
    }
}