using Microsoft.AspNetCore.Mvc;

namespace WebLNCTAssistant.Areas.Colleges.Controllers
{
    [Area("Colleges")]
    [Route("Colleges")]
    public class CollegesController : Controller
    {
        [HttpGet]
        [Route("LNCTM")]
        public async Task<IActionResult> LNCTM()
        {
            return View();
        }

        [HttpGet]
        [Route("LNCTS")]
        public async Task<IActionResult> LNCTS()
        {
            return View();
        }

        [HttpGet]
        [Route("LNCTE")]
        public async Task<IActionResult> LNCTE()
        {
            return View();
        }
    }
}
