using System.ComponentModel.DataAnnotations;

namespace Medical.Entities.Model
{
     public class UserManager
    {
        [Display(Name ="Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhớ đăng nhập")]
        public bool IsPersistent { get; set; }

        public bool LockoutOnFailure { get; set; }

    }
}
