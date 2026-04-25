using Microsoft.AspNetCore.Mvc;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api")]
    public class MenuApiController : Controller
    {
        private readonly IMenu _menu;
        private readonly ITitlemenu _titleMenu;

        public MenuApiController(IMenu menu, ITitlemenu titleMenu)
        {
            _menu = menu;
            _titleMenu = titleMenu;
        }

        /// <summary>
        /// Tạo menu mới
        /// </summary>
        [Route("tao-menu")]
        [HttpPost]
        public async Task<IActionResult> CreateMenu(MenuVM createMenu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors), errors });
                }

                var result = await _menu.Add(createMenu);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Menu tạo thành công." });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi tạo menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách menu với phân trang
        /// </summary>
        [Route("lay-danh-sach-menu")]
        [HttpPost]
        public async Task<IActionResult> GetAllMenu(int page, int pagesize, string search = "")
        {
            try
            {
                var data = await _menu.GetAll(page, pagesize, search);
                return Json(new
                {
                    success = true,
                    data = data.Item2,
                    totalPages = data.totalpages,
                    currentPage = page
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy danh sách menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy menu theo danh mục với hiển thị cây
        /// </summary>
        [Route("lay-menu-theo-danh-muc")]
        [HttpPost]
        public async Task<IActionResult> GetMenuByCategory(int page, int pagesize)
        {
            try
            {
                var data = await _menu.GetAllMenu(page, pagesize);
                return Json(new
                {
                    success = true,
                    data = data.Item2,
                    totalPages = data.totalpages,
                    currentPage = page
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy chi tiết menu
        /// </summary>
        [Route("chi-tiet-menu/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetMenuById(int id)
        {
            try
            {
                var menu = await _menu.GetById(id);
                if (menu == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy menu" });
                }
                return Json(new { success = true, data = menu });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy chi tiết menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật menu
        /// </summary>
        [Route("cap-nhat-menu/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateMenu(int id, MenuVM updateMenu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }

                var result = await _menu.Update(updateMenu, id);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Cập nhật menu thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Xóa menu
        /// </summary>
        [Route("xoa-menu/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var result = await _menu.Delete(id);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Xóa menu thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xóa menu: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách danh mục menu (cho dropdown)
        /// </summary>
        [Route("lay-danh-sach-danh-muc")]
        [HttpGet]
        public async Task<IActionResult> GetAllTitleMenus()
        {
            try
            {
                var titleMenus = await _titleMenu.GetAll(1, 1000, "");
                return Json(new
                {
                    success = true,
                    data = titleMenus.Item2
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lấy danh mục: " + ex.Message });
            }
        }
    }
}
