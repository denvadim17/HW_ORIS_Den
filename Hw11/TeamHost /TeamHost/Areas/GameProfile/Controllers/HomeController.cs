using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.Features.Games.Queries;
using TeamHost.Domain.Entities;
using TeamHost.Persistence.Contexts;

namespace TeamHost.Web.Areas.GameProfile.Controllers;
[Area("GameProfile")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;


    public HomeController(IMediator mediator, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _mediator = mediator;
        _userManager = userManager;
        _context = context;
    }
    // GET
    public async Task<IActionResult> Index([FromRoute] int id)
    {

        var result = await _mediator.Send(new GetGameQuery( id) );
        return View(result);
    }
    
}