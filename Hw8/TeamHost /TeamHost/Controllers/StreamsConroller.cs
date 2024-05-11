using Microsoft.AspNetCore.Mvc;

namespace TeamHost.Controllers;

public class StreamsConroller : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}