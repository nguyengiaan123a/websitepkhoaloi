using System.ComponentModel.DataAnnotations;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.DTO.User
{
    public class TitlemenuVM 
    {
        public int ?Id { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống ")]
        public string title { get; set; } 
        public bool ? status   { get; set;}
        public string ?Thumnail { get; set;}
    }
}
