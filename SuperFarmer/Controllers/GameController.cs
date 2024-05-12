using Microsoft.AspNetCore.Mvc;

namespace SuperFarmer.Controllers
{
    public class GameController : Controller
    {
        public IActionResult PlayerPanel(int playerId)
        {
            return View();
        }
    }
}
