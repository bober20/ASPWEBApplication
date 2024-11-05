using Newtonsoft.Json;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Extensions;
namespace WEB_253503_Soroka.UI.Models;

public class SessionCart : Cart
{
    [JsonIgnore]
    private IHttpContextAccessor _httpContextAccessor;
    
    // public new int Count { get => _cart.Count; }
    //
    // public new double TotalPrice
    // {
    //     get => _cart.TotalPrice;
    // }
    
    public SessionCart(){}
    
    public SessionCart(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public static Cart GetCart(IServiceProvider services)
    {
        var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        var session = httpContext.Session;

        var cart = session.Get<SessionCart>("cart") ?? new SessionCart(httpContextAccessor);
        cart._httpContextAccessor = httpContextAccessor;
        return cart;
    }

    public override void AddToCart(Show show)
    {
        base.AddToCart(show);
        _httpContextAccessor.HttpContext.Session.Set("cart", this);
    }

    public override void RemoveItems(int id)
    {
        base.RemoveItems(id);
        _httpContextAccessor.HttpContext.Session.Set("cart", this);
    }

    public override void ClearAll()
    {
        base.ClearAll();
        _httpContextAccessor.HttpContext.Session.Set("cart", this);
    }
}