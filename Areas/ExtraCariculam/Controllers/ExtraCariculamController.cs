using Microsoft.AspNetCore.Mvc;

namespace WebLNCTAssistant.Areas.ExtraCariculam.Controllers
{
    [Area("ExtraCariculam")]
    [Route("ExtraCariculam")]
    public class ExtraCariculamController : Controller
    {
        [HttpGet]
        [Route("ExtraCariculam")]
        public async Task<IActionResult> ExtraCariculam()
        {
            return View();
        }
    }
}
