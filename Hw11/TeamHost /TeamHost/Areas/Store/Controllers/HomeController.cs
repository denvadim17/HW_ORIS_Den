using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.Features.Games.Queries;
using TeamHost.Domain.Entities;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(GetAllGamesQuery query)
        {
            var result = await _mediator.Send(query);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> BuyGame(int gameId)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null) return NotFound("User not found");

            var user = await _context.UserProfiles
                .Include(u => u.Wallet)
                .Include(u => u.PurchasedGames)
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);
            if (user == null) return NotFound("User not found");

            var game = await _context.Games.FindAsync(gameId);
            if (game == null) return NotFound("Game not found");

            if (user.PurchasedGames.Any(g => g.Id == gameId))
            {
                TempData["ErrorMessage"] = "Игра уже куплена.";
                return RedirectToAction("Index");
            }

            if (user.Wallet.Balance < game.Price)
            {
                TempData["ErrorMessage"] = "Недостаточно средств.";
                return RedirectToAction("Index");
            }

            user.Wallet.Balance -= game.Price;
            user.PurchasedGames.Add(game);

            var transaction = new Transaction
            {
                Amount = game.Price,
                Date = DateTime.Now,
                Type = "Purchase",
                WalletId = user.Wallet.WalletId,
                Description = $"Purchased game: {game.Name}"
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { area = "Wallet" });
        }
    }
}