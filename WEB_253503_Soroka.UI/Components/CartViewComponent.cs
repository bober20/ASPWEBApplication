using Microsoft.AspNetCore.Mvc;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Models;
using WEB_253503_Soroka.UI.Extensions;

namespace WEB_253503_Soroka.UI.Components;

public class CartViewComponent : ViewComponent
{
    private readonly Cart _cart;

    public CartViewComponent(Cart cart)
    {
        _cart = cart;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cart);
    }
}


// public class CartSummary : ViewComponent
// {
//     private IRepository repository;

//     public CartSummary(IRepository repo)
//     {
//         repository = repo;
//     }

//     public IViewComponentResult Invoke()
//     {
//         // Возвращаем данные для представления компонента
//         var cartData = repository.GetAll();
//         return View(cartData);
//     }
// }

