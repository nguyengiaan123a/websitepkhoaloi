using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Data;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;
using websitepkhoaloi.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace websitepkhoaloi.Services.Responsive
{
    public class UserResponsive : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _Mapper;
        private readonly MyDbcontext _dbcontext;

        public UserResponsive(UserManager<ApplicationUser> userManager,IMapper Mapper,MyDbcontext dbcontext)
        {
            _userManager = userManager;
            _Mapper = Mapper;
            _dbcontext = dbcontext;


        }

        public Task<CreateUser> Add(CreateUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<status> CreateUser(CreateUser user)
        {
            status _status = new status();

            try
            {
                var existingUser = await _userManager.FindByNameAsync(user.Username);

                if (existingUser != null)
                {
                    return new status
                    {
                        Status = 0,
                        Message = "Tài khoản đã tồn tại"
                    };
                }

                var newUser = new ApplicationUser
                {
                    UserName = user.Username,
                    FullName = user.FullName,
                    DateCreated= DateTime.Now
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    return new status
                    {
                        Status = 1,
                        Message = "Đăng ký thành công"
                    };
                }

                return new status
                {
                    Status = 0,
                    Message = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }
            catch (Exception ex)
            {
                return new status
                {
                    Status = 0,
                    Message = ex.Message
                };
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateUser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(int totalpages, IReadOnlyList<CreateUser>)> GetAll(int page, int pagesize, string search)
        {
            try
            {
                IQueryable<ApplicationUser> query = _dbcontext.ApplicationUsers.AsNoTracking();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(u => u.UserName.Contains(search) || u.FullName.Contains(search));
                }

                int totalItems = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);

                var users = await query
                    .OrderBy(u => u.DateCreated)
                    .Skip((page - 1) * pagesize)
                    .Take(pagesize).Select(u => new ApplicationUser
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FullName = u.FullName,
                        DateCreated = u.DateCreated
                    })
                    .ToListAsync();

                var userDtos = _Mapper.Map<List<CreateUser>>(users);

                return (totalPages, userDtos);
            }
            catch (Exception ex)
            {
                return (0, new List<CreateUser>());

            }
        }

        public Task Update(CreateUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
