using AutoMapper;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Data;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Services.Responsive
{
    public class MenuResponsive : IMenu
    {
        private readonly MyDbcontext _context;
        private readonly IMapper _mapper;

        public MenuResponsive(MyDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<status> Add(MenuVM entity)
        {
            try
            {
                var menu = _mapper.Map<Menu>(entity);
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
                return new status { Status = 1, Message = "Thêm thành công" };
            }
            catch (Exception ex)
            {
                return new status { Status = 0, Message = ex.Message };
            }
        }

        public async Task<status> Delete(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);
                if (menu == null)
                {
                    return new status { Status = 0, Message = "Menu không tồn tại" };
                }
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
                return new status { Status = 1, Message = "Xóa thành công" };
            }
            catch (Exception ex)
            {
                return new status { Status = 0, Message = ex.Message };
            }
        }

        public async Task<(int totalpages, IReadOnlyList<MenuVM>)> GetAll(int page, int pagesize, string search)
        {
            try
            {
                var query = _context.Menus
                    .Include(m => m.titlemenu)
                    .AsNoTracking()
                    .AsQueryable();

                // Tìm kiếm theo tên hoặc URL
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(m => m.title.Contains(search) || m.url.Contains(search));
                }

                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);

                var menus = await query
                    .OrderByDescending(m => m.DateCreated)
                    .Skip((page - 1) * pagesize)
                    .Take(pagesize)
                    .ToListAsync();

                var result = _mapper.Map<List<MenuVM>>(menus);

                return (totalPages, result);
            }
            catch (Exception ex)
            {
                return (0, new List<MenuVM>());
            }
        }

      public async Task<(int totalpages, IReadOnlyList<ListTitleMenu>)> GetAllMenu(int page, int pagesize)
{
    try
    {
        var query = _context.Menus
            .Include(m => m.titlemenu)  // ✅ Fix: Include navigation property
            .AsNoTracking()
            .AsQueryable();

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);

        var menus = await query
            .Skip((page - 1) * pagesize)
            .Take(pagesize).OrderBy(g => g.order) 
            .ToListAsync();

        var result = menus
            .GroupBy(m => m.TitlemenuId) // ✅ Fix: Group by TitlemenuId and order by DateCreated
            .Select(g => new ListTitleMenu
            {
                Id = g.First().titlemenu.Id,
                Title = g.First().titlemenu?.title ?? "N/A",  // ✅ Fix: Get actual title
                Menus = _mapper.Map<List<MenuVM>>(g.ToList())
            })
            .ToList();

        return (totalPages, result);
    }
    catch (Exception ex)
    {
        return (0, new List<ListTitleMenu>());
    }
}

        public async Task<MenuVM> GetById(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);
                if (menu == null)
                {
                    return null;
                }
                return _mapper.Map<MenuVM>(menu);
            }
            catch
            {
                return null;
            }
         
        }

        public async Task<status> Update(MenuVM entity, int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);
                if (menu == null)
                {
                    return new status { Status = 0, Message = "Menu không tồn tại" };
                }
                _mapper.Map(entity, menu);
                await _context.SaveChangesAsync();
                return new status { Status = 1, Message = "Cập nhật thành công" };
            }
            catch (Exception ex)
            {
                return new status { Status = 0, Message = ex.Message };
            }
        }
    }
}