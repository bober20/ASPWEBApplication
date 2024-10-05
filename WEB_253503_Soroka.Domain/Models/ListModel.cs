namespace WEB_253503_Soroka.Domain.Models;

public class ListModel<T>
{
    // запрошенный список объектов
    public List<T> Items { get; set; }
    // номер текущей страницы
    public int CurrentPage { get; set; } = 1;
    // общее количество страниц
    public int TotalPages { get; set; } = 1;
}