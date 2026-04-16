using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("/trang-quan-tri/trang-chu")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
