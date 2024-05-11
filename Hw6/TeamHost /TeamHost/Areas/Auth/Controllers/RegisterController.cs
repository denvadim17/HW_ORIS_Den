using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Web.Areas.Auth.Models;

namespace TeamHost.Web.Areas.Auth.Controllers;

[Area("Auth")]
[AllowAnonymous]
public class RegisterController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">Сервис работы с пользователем</param>
    /// <param name="signInManager">Сервис работы с аутентификацией</param>
    public RegisterController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
        
    public IActionResult Index()
    {
        return View();
    }
        
    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="model">Модель</param>
    /// <returns>-</returns>
    [HttpPost]
    public async Task<IActionResult> Index([FromForm] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            
            var result = await _userManager.CreateAsync(user, model.Password);

            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("index", "home", new
                {
                    Area = "",
                });
            }

            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
}