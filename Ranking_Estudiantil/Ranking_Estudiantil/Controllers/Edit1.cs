using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ranking_Estudiantil.Controllers
{
    [Authorize]
    public class Edit1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
