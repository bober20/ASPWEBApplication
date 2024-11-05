using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Soroka.UI.Models;

namespace WEB_253503_Soroka.UI.Controllers;

public class HomeController : Controller
{
    [ViewData]
    public string? LabName { get; set; }

    [ViewData]
    public List<ListDemo> ListDemos { get; set; } = new() {
        new ListDemo { Id = 0, Name = "Item1" },
        new ListDemo { Id = 1, Name = "Item2" },
        new ListDemo { Id = 2, Name = "Item3" }
    };

    public IActionResult Index(SessionCart sessionCart)
    {
        LabName = "Лабораторная работа 2";

        return View(ListDemos);
    }
}
