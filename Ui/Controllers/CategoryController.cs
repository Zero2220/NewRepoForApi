using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
