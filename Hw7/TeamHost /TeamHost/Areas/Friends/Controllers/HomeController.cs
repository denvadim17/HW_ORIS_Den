using Microsoft.AspNetCore.Mvc;

namespace TeamHost.Web.Areas.Friends.Controllers;
[Area("Friends")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}