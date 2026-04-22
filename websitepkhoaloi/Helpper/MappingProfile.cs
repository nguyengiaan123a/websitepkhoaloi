using AutoMapper;
using websitepkhoaloi.Models.DTO.User;
using websitepkhoaloi.Models.Enitity;

namespace websitepkhoaloi.Helpper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // map user
            CreateMap<ApplicationUser, CreateUser>().ReverseMap();
             CreateMap<Titlemenu, TitlemenuVM>().ReverseMap();
        }
    }
}
