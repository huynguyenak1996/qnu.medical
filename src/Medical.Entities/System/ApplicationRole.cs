using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medical.Entities.System
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Ngày tạo")]

        public DateTime CreatedDate { get; set; } = DateTime.Parse("01/01/1990");

        [Display(Name = "Người tạo")]

        public string CreatedByUser { get; set; }

        [Display(Name = "Ngày chỉnh sửa")]

        public DateTime ModifiedDate { get; set; } = DateTime.Parse("01/01/1990");

        [Display(Name = "Người chỉnh sửa")]

        public string ModifiedByUser { get; set; }
    }
}
