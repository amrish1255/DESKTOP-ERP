using Microsoft.AspNetCore.Mvc;

namespace WebCode.Controllers;

public class PartyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Create")]
    public IActionResult CreatePost()
    {
        return View("Create");
    }
}
