using Microsoft.AspNetCore.Mvc;

namespace MicroNpmRegistry.Controllers
{
    [Route("AdminUi")]
    public class AdminController : Controller
    {
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
