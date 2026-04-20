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

        public UserResponsive(UserManager<ApplicationUser> userManager, IMapper Mapper, MyDbcontext dbcontext)
        {
            _userManager = userManager;
            _Mapper = Mapper;
            _dbcontext = dbcontext;
        }


        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
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
                    DateCreated = DateTime.Now,
                    
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


        /// <summary>
        /// Xóa người dùng theo ID
        /// </summary>
        public async Task<status> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new status
                    {
                        Status = 0,
                        Message = "Không tìm thấy người dùng"
                    };
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new status
                    {
                        Status = 1,
                        Message = "Xóa người dùng thành công"
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


        /// <summary>
        /// Lấy thông tin chi tiết người dùng theo ID
        /// </summary>
        public async Task<CreateUser> Get(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return null;
                }

                return _Mapper.Map<CreateUser>(user);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng với phân trang và tìm kiếm
        /// </summary>
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
                    .OrderByDescending(u => u.DateCreated)
                    .Skip((page - 1) * pagesize)
                    .Take(pagesize)
                    .Select(u => new ApplicationUser
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FullName = u.FullName,
                        DateCreated = u.DateCreated,
                    
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


        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        public async Task<status> Update(string id, CreateUser updateUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new status
                    {
                        Status = 0,
                        Message = "Không tìm thấy người dùng"
                    };
                }

                user.FullName = updateUser.FullName;
                user.UserName = updateUser.Username;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Nếu có mật khẩu mới, cập nhật mật khẩu
                    if (!string.IsNullOrEmpty(updateUser.Password))
                    {
                        var removeResult = await _userManager.RemovePasswordAsync(user);
                        if (removeResult.Succeeded)
                        {
                            var addResult = await _userManager.AddPasswordAsync(user, updateUser.Password);
                            if (!addResult.Succeeded)
                            {
                                return new status
                                {
                                    Status = 0,
                                    Message = "Cập nhật mật khẩu thất bại"
                                };
                            }
                        }
                    }

                    return new status
                    {
                        Status = 1,
                        Message = "Cập nhật người dùng thành công"
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

    }
}
