using System.ComponentModel.DataAnnotations;

namespace websitepkhoaloi.Models.DTO.User
{
    public class CreateUser
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = ("Vui lòng nhập tên người dùng"))]
        public string FullName { get; set; }

        [Required(ErrorMessage = ("Vui lòng nhập tài khoản người dùng"))]

        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu người dùng")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, bao gồm ít nhất 1 chữ in hoa, 1 số và 1 ký tự đặc biệt")]
        public string Password { get; set; }

    }
}
