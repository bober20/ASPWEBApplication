using WEB_253503_Soroka.Domain.Entities;
namespace WEB_253503_Soroka.Domain.Models;

public class Cart
{
    public List<CartItem> CartItems { get; set; }
    public int TotalPrice
    {
        get => CartItems.Sum(item => item.Show.Price * item.Count);
    }

    public int Count
    {
        get => CartItems.Sum(item => item.Count);
    }

    public Cart()
    {
        CartItems = new List<CartItem>();
    }

    public virtual void AddToCart(Show show)
    {
        var cartItem = CartItems.FirstOrDefault(item => item.Show.Id == show.Id);
        if (cartItem is null)
        {
            CartItems.Add(new CartItem { Show = show, Count = 1 });
        }
        else
        {
            cartItem.Count++;
        }
    }
    
    public virtual void RemoveItems(int id)
    {
        var cartItem = CartItems.FirstOrDefault(item => item.Show.Id == id);
        if (cartItem is not null)
        {
            if (cartItem.Count > 1)
            {
                cartItem.Count--;
            }
            else
            {
                CartItems.Remove(cartItem);
            }
        }
    }
    
    public virtual void ClearAll()
    {
        CartItems.Clear();
    }
}