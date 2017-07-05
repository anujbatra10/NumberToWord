using NumbersToWordsConverter.Models;
using NumbersToWordsConverter.NumberToWordService;

using System.Web.Mvc;

namespace NumbersToWordsConverter.Controllers
{
    public class HomeController : Controller
    {
        private IService serviceClient;
        public HomeController(IService wcfServiceClient)
        {
            serviceClient = wcfServiceClient;
        }

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Controller post method to convert number to word.
        /// </summary>
        /// <param name="userDetail">Contains the details of user input -name and number entered to convert.</param>
        /// <returns>number in words</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserModel userDetail)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //Call to WCF service to get the result.
                    var result = serviceClient.ConvertToWords(userDetail.Number);
                    ViewBag.Result = userDetail.Name + "<br/>" + result;
                }
                catch { ViewBag.Result = "Error while processing the request. Please try again."; }
            }
            else
            {
                ViewBag.Result = "Please enter the correct details.";
            }

            return View();
        }




    }
}