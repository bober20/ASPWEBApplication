using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Extensions;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly IShowService _showService;
    private Cart _cart;
    public CartController(IShowService showService, Cart cart)
    {
        _showService = showService;
        _cart = cart;
    }
    
    [Route("[controller]/add/{id:int}")]
    public async Task<ActionResult> Add(int id, string returnUrl)
    {
        var data = await _showService.GetShowByIdAsync(id);
        var a = 0;
        if (data.Successfull)
        {
            _cart.AddToCart(data.Data);
        }
        return Redirect(returnUrl);
    }

    [Route("[controller]/delete/{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        _cart.RemoveItems(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult Index()
    {
        return View(_cart);
    }
}