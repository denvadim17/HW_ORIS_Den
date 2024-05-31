using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Web.Areas.Streams.Controllers
{
    [Area("Streams")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var videos = await _context.Videos.ToListAsync();
            return View(videos);
        }

        public async Task<IActionResult> Watch(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }
    }
}
