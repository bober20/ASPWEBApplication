using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_Soroka.UI.Areas.Admin.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}