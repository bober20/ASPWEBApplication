using System.ComponentModel.DataAnnotations;

namespace WEB_253503_Soroka.SSR.Models;

public class NewCounterModel
{
    [Required(ErrorMessage = "Count should be between 1 and 10")]
    [Range(1, 10)]
    public int Count { get; set; }
}