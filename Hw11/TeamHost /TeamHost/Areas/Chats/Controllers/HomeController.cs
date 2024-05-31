using Microsoft.AspNetCore.Mvc;

namespace TeamHost.Web.Areas.Chats.Controllers;
[Area("Chats")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}