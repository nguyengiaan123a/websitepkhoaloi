using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;

namespace websitepkhoaloi.Services.Interface
{
    public interface IUser : IGenericRepository<CreateUser>
    {
        public Task<status> CreateUser(CreateUser user );
    }
}
