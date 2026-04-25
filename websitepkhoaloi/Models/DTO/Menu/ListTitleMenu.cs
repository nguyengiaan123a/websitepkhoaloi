using System.ComponentModel.DataAnnotations;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.DTO.User
{
    public class  ListTitleMenu
    {
        public int  Id { get; set;}
        public string Title { get; set;}

        public List<MenuVM> Menus { get; set;}
    }
}
