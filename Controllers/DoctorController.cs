using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MEDManager.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        // GET: DoctorController
        public ActionResult Index()
        {
            return View();
        }
    }
}
