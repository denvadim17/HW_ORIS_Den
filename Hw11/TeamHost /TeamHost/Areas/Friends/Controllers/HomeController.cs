using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHost.Domain.Entities;
using TeamHost.Persistence.Contexts;
using TeamHost.Web.Areas.Friends.Models;

namespace TeamHost.Web.Areas.Friends.Controllers;
[Area("Friends")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string searchTerm)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        var user = await _context.UserProfiles
            .Include(u => u.Friendships)
            .ThenInclude(f => f.Friend)
            .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        var friends = user.Friendships
            .Where(f => f.IsAccepted)
            .Select(f => f.Friend)
            .ToList();

        var friendRequests = await _context.Friendships
            .Where(f => f.FriendId == user.Id && !f.IsAccepted)
            .Include(f => f.User)
            .ToListAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            var searchResults = await _context.UserProfiles
                .Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm))
                .ToListAsync();

            return View(new FriendsViewModel { Friends = friends, SearchResults = searchResults, FriendRequests = friendRequests, SearchTerm = searchTerm });
        }

        return View(new FriendsViewModel { Friends = friends, FriendRequests = friendRequests });
    }

    public async Task<IActionResult> AddFriend(int friendId)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        if (user == null || await _context.UserProfiles.FindAsync(friendId) == null)
        {
            return NotFound();
        }

        if (_context.Friendships.Any(f => f.UserId == user.Id && f.FriendId == friendId))
        {
            return BadRequest("Friend request already sent or already friends");
        }

        var friendship = new Friendship
        {
            UserId = user.Id,
            FriendId = friendId,
            IsAccepted = false
        };

        _context.Friendships.Add(friendship);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AcceptFriend(int friendId)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        var friendship = await _context.Friendships
            .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == user.Id);

        if (friendship == null)
        {
            return NotFound();
        }
        
        friendship.IsAccepted = true;

        
        var reverseFriendship = new Friendship
        {
            UserId = user.Id,
            FriendId = friendId,
            IsAccepted = true
        };

        _context.Friendships.Add(reverseFriendship);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RejectFriend(int friendId)
    {
        var identityUser = await _userManager.GetUserAsync(User);
        var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

        var friendship = await _context.Friendships
            .FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == user.Id);

        if (friendship == null)
        {
            return NotFound();
        }

        _context.Friendships.Remove(friendship);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}