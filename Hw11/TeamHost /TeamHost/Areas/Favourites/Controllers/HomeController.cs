using Microsoft.AspNetCore.Mvc;

namespace TeamHost.Web.Areas.Favourites.Controllers;

[Area("Favourites")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}