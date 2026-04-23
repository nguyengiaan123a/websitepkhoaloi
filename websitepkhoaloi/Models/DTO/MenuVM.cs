using System.ComponentModel.DataAnnotations;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.DTO.User
{
    public class  MenuVM
    {
        public int ?Id { get; set;}  
         public string thumnail { get; set;}
        public string url { get; set;}
        public int order { get; set;}
        public int TitlemenuId { get; set;}

    }
}
