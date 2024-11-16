using Microsoft.AspNetCore.Mvc;

namespace MEDManager.Models
{
    public class SupportController : Controller
    {
        // GET: SupportController
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
