using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MEDManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace MEDManager.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(); // retourne la vue Index.cshtml qui se trouve dans [controller]/[action].cshtml => /Views/Home/Index.cshtml /Views/Account/Register.cshtml
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
