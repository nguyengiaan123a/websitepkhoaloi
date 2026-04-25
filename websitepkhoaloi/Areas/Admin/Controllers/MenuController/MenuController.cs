using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        [Route("/trang-quan-tri/quan-ly-menu")]
        public IActionResult IndexMenu()
        {
            return View();
        }
    }
}
