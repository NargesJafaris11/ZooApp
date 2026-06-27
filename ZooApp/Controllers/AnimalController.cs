using Microsoft.AspNetCore.Mvc;

namespace ZooApp.Controllers;

public class AnimalController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}