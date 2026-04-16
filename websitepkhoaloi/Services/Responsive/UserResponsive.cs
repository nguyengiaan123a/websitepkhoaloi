using AutoMapper;
using Microsoft.AspNetCore.Identity;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;
using websitepkhoaloi.Services.Interface;

namespace websitepkhoaloi.Services.Responsive
{
    public class UserResponsive : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _Mapper;


        public UserResponsive(UserManager<ApplicationUser> userManager,IMapper Mapper)
        {
            _userManager = userManager;
            _Mapper = Mapper;
      
        }

        public Task<ApplicationUser> Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<status> CreateUser(CreateUser user)
        {
            status _status = new status();
            try
            {
            
                var existingUser = await _userManager.FindByEmailAsync(user.Username);
                if (existingUser != null)
                {
                    _status.Status = 0;
                    _status.Message = "Username đã tồn tại";
                    return _status;
                }
                var newUser = _Mapper.Map<ApplicationUser>(user);
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    _status.Status = 1;
                    _status.Message = "Đăng ký thành công";
                    return _status;
                }
                else
                {
                    _status.Status = 0;
                    _status.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                    return _status;
                }
     

            }
            catch (Exception ex)
            {
                _status.Status = 0;
                _status.Message = ex.Message;
            }
           return new status { Status = 0, Message = "Error" };

        }

        public Task Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ApplicationUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
