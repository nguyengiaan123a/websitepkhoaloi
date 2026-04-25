using Azure;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;

namespace websitepkhoaloi.Services.Interface
{
    public interface IMenu : IGenericRepository<MenuVM>
    {
        public Task<(int totalpages, IReadOnlyList<ListTitleMenu>)> GetAllMenu(int page,int pagesize);

       
    }
}
