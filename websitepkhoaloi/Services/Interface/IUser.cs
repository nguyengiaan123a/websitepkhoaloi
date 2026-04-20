using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;

namespace websitepkhoaloi.Services.Interface
{
    public interface IUser 
    {
        Task<status> CreateUser(CreateUser user);
        Task<CreateUser> Get(string id);
        Task<status> Update(string id, CreateUser updateUser);
        Task<status> Delete(string id);

        Task<(int totalpages, IReadOnlyList<CreateUser>)> GetAll(int page, int pagesize, string search); // int totalpages, IReadOnlyList<CreateUser>
     
    }
}
