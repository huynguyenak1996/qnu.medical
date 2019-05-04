using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Medical.Entities.System
{
    public class ApplicationClaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Mã Chức Năng")]
        public string ClaimValue { get; set; }

        [Display(Name = "Tên Chức Năng")]
        public string ClaimName { get; set; }

        [Display(Name = "Loại Chức Năng")]
        public string ClaimType { get; set; }


    }
}
