using Microsoft.AspNetCore.Mvc;

namespace Ranking_Estudiantil.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
