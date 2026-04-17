using Microsoft.AspNetCore.Mvc;
using websitepkhoaloi.Data;
using websitepkhoaloi.Migrations;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user=user;

        }
        // GET: Admin/User
        [Route("/trang-quan-tri/quan-li-nguoi-dung")]
        public IActionResult IndexUser()
        {
            return View();
        }
        [Route("/api/dang-ky-tai-khoan")]
        public async Task<IActionResult> CreateUser(CreateUser create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { status = false, message = string.Join(", ", errors), errors });
                }
                var result = await _user.CreateUser(create);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Người dùng đã được tạo thành công." });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi tạo người dùng: " + ex.Message });
            }

        }

        [Route("/api/lay-danh-sach-nguoi-dung")]
        public async Task<IActionResult> GetAllUser(int page,int pagesize,string search)
        {
            try
            {
                var users = await _user.GetAll(page,pagesize,search);
                return Json(new { success = true, data = users ,totalpage=users.totalpages });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy danh sách người dùng: " + ex.Message });
            }
        }

    }
    
}
