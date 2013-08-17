using System.Web.Mvc;
using Core;

namespace UI.Controllers
{
    [HandleError]
    [VisitorRetrievalFilter(Order = 1)]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Please verify your info.";
            var visitor = new VisitorBuilder().BuildVisitor();
            return View(visitor);
        }

        [HttpPost]
        public ActionResult Index(Visitor visitor)
        {
            if (!ModelState.IsValid)
            {
                return View(visitor);
            }

            new VisitorRepositoryFactory().BuildRepository().Save(visitor);
            TempData.Add("message", "Your visit has been logged.");
            return RedirectToAction("index");
        }
    }
}