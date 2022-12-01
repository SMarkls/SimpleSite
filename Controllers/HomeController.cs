using Microsoft.AspNetCore.Mvc;

namespace Examine.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Repertory() => View();
}