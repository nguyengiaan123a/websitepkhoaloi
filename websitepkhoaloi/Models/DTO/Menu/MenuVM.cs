using System.ComponentModel.DataAnnotations;
using websitepkhoaloi.Models.Common;

namespace websitepkhoaloi.Models.DTO.User
{
    public class MenuVM
    {
        public int ?Id { get; set; }

        [Required(ErrorMessage = "Tên menu không được để trống")]
        public string title { get; set; }

        [Required(ErrorMessage = "URL không được để trống")]
        public string Url { get; set; }

        public string Thumnail { get; set; }

        public int Order { get; set; }

        [Required(ErrorMessage = "Danh mục menu không được để trống")]
        public int TitlemenuId { get; set; }

        public bool Status { get; set; } = true;

    }
}
