namespace WEB_253503_Soroka.Domain.Entities;

public class Show {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Genre? Genre { get; set; }
    public int Price { get; set; }
    public string? Image { get; set; }
}