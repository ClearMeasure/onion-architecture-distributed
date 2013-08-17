using System.Web.Mvc;
using Core;

namespace UI.Controllers
{
    [HandleError]
    [ApplicantRetrievalFilter(Order = 1)]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Please verify your info.";
            var visitor = new ApplicantBuilder().BuildApplicant();
            return View(visitor);
        }

        [HttpPost]
        public ActionResult Index(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return View(applicant);
            }

            new ApplicantRepositoryFactory().BuildRepository().Save(applicant);
            new CreditCardApplicationRepositoryFactory().BuildRepository()
                .SaveApplicationFor(applicant);

            TempData.Add("message", "Your application has been logged.");
            return RedirectToAction("index");
        }
    }
}