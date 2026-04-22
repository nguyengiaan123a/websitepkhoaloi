using AutoMapper;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Data;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Services.Responsive
{
    public class TitleMenuResponsive : ITitlemenu
    {
        private readonly MyDbcontext _context;
        private readonly IMapper _mapper;

        public TitleMenuResponsive(MyDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<status> Add(TitlemenuVM entity)
        {
            try
            {
                var titlemenu = _mapper.Map<Titlemenu>(entity);
                _context.Titlemenus.Add(titlemenu);
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
                var titlemenu = await _context.Titlemenus.FindAsync(id);
                if (titlemenu == null)
                {
                    return new status { Status = 0, Message = "Không tìm thấy tiêu đề menu" };
                }

                _context.Titlemenus.Remove(titlemenu);
                await _context.SaveChangesAsync();

                return new status { Status = 1, Message = "Xóa thành công" };
            }
            catch (Exception ex)
            {
                return new status { Status = 0, Message = ex.Message };
            }
        }

        public async Task<(int totalpages, IReadOnlyList<TitlemenuVM>)> GetAll(int page, int pagesize, string search)
        {
            try
            {
                IQueryable<Titlemenu> query = _context.Titlemenus.AsNoTracking();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(t => t.title.Contains(search));
                }

                int totalItems = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);

                var data = await query
                    .OrderByDescending(x => x.DateCreated)
                    .Skip((page - 1) * pagesize)
                    .Take(pagesize).Select(x=> new Titlemenu
                    {
                        Id = x.Id,
                        title = x.title,
                        thumnail = x.thumnail,
                        
                    })
                    .ToListAsync();

                var result = _mapper.Map<List<TitlemenuVM>>(data);

                return (totalPages, result);
            }
            catch (Exception)
            {
                return (0, new List<TitlemenuVM>());
            }
        }

        public async Task<status> Update(TitlemenuVM entity, int id)
        {
            try
            {
                var titlemenu = await _context.Titlemenus.FindAsync(id);
                if (titlemenu == null)
                {
                    return new status { Status = 0, Message = "Không tìm thấy tiêu đề menu" };
                }

                _mapper.Map(entity, titlemenu);

                _context.Titlemenus.Update(titlemenu);
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