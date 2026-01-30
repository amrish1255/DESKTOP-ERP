using Microsoft.AspNetCore.Mvc;

namespace WebCode.Controllers;

public class ReportsController : Controller
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

    public IActionResult SalesReport()
    {
        return View();
    }

    public IActionResult GstReport()
    {
        return View();
    }

    public IActionResult OutstandingReport()
    {
        return View();
    }
}
