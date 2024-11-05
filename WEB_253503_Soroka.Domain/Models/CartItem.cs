using WEB_253503_Soroka.Domain.Entities;
namespace WEB_253503_Soroka.Domain.Models;

public class CartItem
{
    public Show Show { get; set; }
    public int Count { get; set; }
}