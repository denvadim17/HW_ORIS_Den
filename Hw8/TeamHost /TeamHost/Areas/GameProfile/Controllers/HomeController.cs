using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.Features.Games.Queries;

namespace TeamHost.Web.Areas.GameProfile.Controllers;
[Area("GameProfile")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // GET
    public async Task<IActionResult> Index([FromRoute] int id)
    {

        var result = await _mediator.Send(new GetGameQuery( id) );
        return View(result);
    }
}