using Microsoft.AspNetCore.Mvc;

namespace WebLNCTAssistant.Areas.Aboutus.Controllers
{
    [Area("Aboutus")]
    [Route("Aboutus")]
    public class Aboutus : Controller
    {
        [HttpGet]
        [Route("Aboutus")]
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}
