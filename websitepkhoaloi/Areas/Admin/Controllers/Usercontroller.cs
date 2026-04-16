using Microsoft.AspNetCore.Mvc;

namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        // GET: Admin/User
        [Route("/trang-quan-tri/quan-li-nguoi-dung")]
        public IActionResult IndexUser()
        {
            return View();
        }

   
    }
    
}
