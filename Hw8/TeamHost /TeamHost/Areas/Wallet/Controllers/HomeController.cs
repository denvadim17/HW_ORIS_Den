using Microsoft.AspNetCore.Mvc;

namespace TeamHost.Web.Areas.Wallet.Controllers;
[Area("Wallet")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}