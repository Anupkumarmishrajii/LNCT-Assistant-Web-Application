using Microsoft.AspNetCore.Mvc;

namespace WebLNCTAssistant.Areas.Placements.Controllers
{
    [Area("Placements")]
    [Route("Placements")]
    public class PlacementsConyroller : Controller
    {
        [HttpGet]
        [Route("Placements")]
        public IActionResult Placements()
        {
            return View();
        }
    }
}
