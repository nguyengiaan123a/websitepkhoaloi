using Microsoft.AspNetCore.Identity;
using websitepkhoaloi.Helpper;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.Enitity
{
    public class Titlemenu : BaseDomainEntity
    {
        
       public Titlemenu()
        {
            menus = new List<Menu>();
        }
       public string thumnail { get; set;}
       public List<Menu> menus { get; set;}

    }
}
