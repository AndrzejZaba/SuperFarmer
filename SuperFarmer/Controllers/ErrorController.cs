using Microsoft.AspNetCore.Mvc;

namespace SuperFarmer.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult Index()
        {
            return View("Error");
        }
    }
}
