using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medical.Entities.Model
{
     public class InforRegister
    {
        [Display(Name ="Email hoặc Sdt")]
        [Required]
        public string EmailOrPhoneNumber { get; set; }

        [Display(Name ="Mã xác thực")]
        public string CodeValidate { get; set; }

        [Display(Name ="Hạn mã xác thực")]
        public DateTime Expried { get; set; }

        [Display(Name ="Đã xác thực")]
        public bool IsValidate { get; set; }
    }
}
