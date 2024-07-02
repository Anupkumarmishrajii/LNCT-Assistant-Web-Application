using Microsoft.AspNetCore.Mvc;

namespace WebLNCTAssistant.Areas.Facilties.Controllers
{
    [Area("Facilties")]
    [Route("Facilties")]
    public class FaciltiesController : Controller
    {
        [HttpGet]
        [Route("Facilties")]
        public IActionResult Facilties()
        {
            return View();
        }
    }
}
