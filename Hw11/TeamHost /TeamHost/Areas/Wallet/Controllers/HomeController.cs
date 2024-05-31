using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHost.Domain.Entities;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Web.Areas.Wallet.Controllers;
[Area("Wallet")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var identityUser = await _userManager.GetUserAsync(User);
        if (identityUser == null) return NotFound("User not found");

        var user = await _context.UserProfiles
            .Include(u => u.Wallet)
            .Include(u => u.Cards)
            .Include(u => u.Wallet.Transactions) 
            .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(decimal amount, int cardId)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        if (identityUser == null) return NotFound("User not found");

        var user = await _context.UserProfiles
            .Include(u => u.Wallet)
            .Include(u => u.Cards)
            .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        var card = user.Cards.FirstOrDefault(c => c.CardId == cardId);
        if (card == null) return NotFound("Card not found");
        if (card.Balance < amount) return BadRequest("Insufficient funds on card");

        user.Wallet.Balance += amount;
        card.Balance -= amount;

        var transaction = new Transaction
        {
            Amount = amount,
            Date = DateTime.Now,
            Type = "Deposit",
            WalletId = user.Wallet.WalletId
        };

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return RedirectToAction("index", "home", new
        {
            Area = "Wallet",
        });
    }

    [HttpPost]
    public async Task<IActionResult> Withdraw(decimal amount, int cardId)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        if (identityUser == null) return NotFound("User not found");

        var user = await _context.UserProfiles
            .Include(u => u.Wallet)
            .Include(u => u.Cards)
            .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        var card = user.Cards.FirstOrDefault(c => c.CardId == cardId);
        if (card == null) return NotFound("Card not found");
        if (user.Wallet.Balance < amount) return BadRequest("Insufficient funds in wallet");

        user.Wallet.Balance -= amount;
        card.Balance += amount;

        var transaction = new Transaction
        {
            Amount = amount,
            Date = DateTime.Now,
            Type = "Withdrawal",
            WalletId = user.Wallet.WalletId
        };

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return RedirectToAction("index", "home", new
        {
            Area = "Wallet",
        });
    }
}