using Microsoft.AspNetCore.Identity;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.Enitity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime DateCreated { get; set; }


    }
}
