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

        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        [Route("/api/dang-ky-tai-khoan")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser create)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors), errors });
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

        /// <summary>
        /// Lấy danh sách tất cả người dùng với phân trang
        /// </summary>
        [Route("/api/lay-danh-sach-nguoi-dung")]
        [HttpPost]
        public async Task<IActionResult> GetAllUser(int page, int pagesize, string search = "")
        {
            try
            {
                var users = await _user.GetAll(page, pagesize, search);
                return Json(new { 
                    success = true, 
                    data = users.Item2,
                    totalPages = users.totalpages,
                    currentPage = page 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy danh sách người dùng: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một người dùng
        /// </summary>
        [Route("/api/chi-tiet-nguoi-dung/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _user.Get(id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy người dùng" });
                }
                return Json(new { success = true, data = user });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy thông tin người dùng: " + ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        [Route("/api/cap-nhat-nguoi-dung/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string id, CreateUser updateUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                    .Where(kv => kv.Key != "Password")  // Bỏ qua key "Password"
                    .SelectMany(kv => kv.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
    
                    if (errors.Any())  // Chỉ trả về lỗi nếu còn lỗi khác ngoài Password
                    {
                         return Json(new { success = false, message = string.Join(", ", errors) });
                    }
    
                }

                var result = await _user.Update(id, updateUser);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Cập nhật người dùng thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật người dùng: " + ex.Message });
            }
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        [Route("/api/xoa-nguoi-dung/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var result = await _user.Delete(id);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Xóa người dùng thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xóa người dùng: " + ex.Message });
            }
        }

    

    }
}
