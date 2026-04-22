using Microsoft.AspNetCore.Mvc;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TitleMenuController : Controller
    {
        private readonly ITitlemenu _titleMenu;

        public TitleMenuController(ITitlemenu titleMenu)
        {
            _titleMenu = titleMenu;
        }

        /// <summary>
        /// Hiển thị trang quản lý menu chính
        /// </summary>
        [Route("/trang-quan-tri/quan-li-menu-chinh")]
        public IActionResult IndexTitleMenu()
        {
            return View();
        }

        /// <summary>
        /// Tạo menu chính mới
        /// </summary>
        [Route("/api/tao-menu-chinh")]
        [HttpPost]
        public async Task<IActionResult> CreateTitleMenu(TitlemenuVM createTitleMenu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors), errors });
                }
                var result = await _titleMenu.Add(createTitleMenu);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Menu chính đã được tạo thành công." });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi tạo menu chính: " + ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả menu chính với phân trang
        /// </summary>
        [Route("/api/lay-danh-sach-menu-chinh")]
        [HttpPost]
        public async Task<IActionResult> GetAllTitleMenu(int page, int pagesize, string search = "")
        {
            try
            {
                var data = await _titleMenu.GetAll(page, pagesize, search);
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
                return Json(new { success = false, message = "Lỗi khi lấy danh sách menu chính: " + ex.Message });
            }
        }

   
        /// <summary>
        /// Cập nhật thông tin menu chính
        /// </summary>
        [Route("/api/cap-nhat-menu-chinh/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTitleMenu(int id,  TitlemenuVM updateTitleMenu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }

                var result = await _titleMenu.Update(updateTitleMenu, id);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Cập nhật menu chính thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật menu chính: " + ex.Message });
            }
        }

    
     
        /// <summary>
        /// Xóa menu chính
        /// </summary>
        [Route("/api/xoa-menu-chinh/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTitleMenu(int id)
        {
            try
            {
                var result = await _titleMenu.Delete(id);
                if (result.Status == 1)
                {
                    return Json(new { success = true, message = "Xóa menu chính thành công" });
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi xóa menu chính: " + ex.Message });
            }
        }
    }


}
