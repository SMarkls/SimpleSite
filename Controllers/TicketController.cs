using Examine.Data.DataBase;
using Examine.Models;
using Examine.Service;
using Microsoft.AspNetCore.Mvc;

namespace Examine.Controllers;

public class TicketController : Controller
{
    private readonly RegistryService service;
    private readonly ApplicationDbContext context;

    public TicketController(RegistryService service, ApplicationDbContext context)
    {
        this.service = service;
        this.context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // вычисление свободных мест на каждом из представлений.
        var goldenFever = 35 - context.Users.Count(x => x.Presentation == "Золотая лихорадка");
        var welcome = 50 - context.Users.Count(x => x.Presentation == "Добро пожаловать на сеанс!");
        var fire = 50 -  context.Users.Count(x => x.Presentation == "Языки пламени");
        ViewBag.Fever = goldenFever;
        ViewBag.Welcome = welcome;
        ViewBag.Fire = fire;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Registry(RegistryViewModel model)
    {
        if (ModelState.IsValid)
        {
            string key = await service.RegisterAsync(model);
            return View("Registry", key);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Unregistry(UnregisrtyViewModel model)
    {
        if (ModelState.IsValid)
        {
            string response = await service.UnregistryAsync(model);
            return View("Registry", response);
        }

        return RedirectToAction("Index");
    }
}