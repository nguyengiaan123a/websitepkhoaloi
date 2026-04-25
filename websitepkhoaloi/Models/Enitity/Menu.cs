using Microsoft.AspNetCore.Identity;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.Enitity
{
    public class Menu : BaseDomainEntity
    {
        public string thumnail { get; set; }
        public string url { get; set; }
        public int order { get; set; }

        public int TitlemenuId { get; set; }

        public Titlemenu titlemenu { get; set; }
    }
}
