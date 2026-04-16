using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using websitepkhoaloi.Models.Enitity;

namespace websitepkhoaloi.Data
{
    public class MyDbcontext : IdentityDbContext<ApplicationUser>
    {
        public MyDbcontext(DbContextOptions<MyDbcontext> options) : base(options)
        {
        }
    }
}
