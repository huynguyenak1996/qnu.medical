using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Medical.Entities.Model
{
    public class SoTiem
    {

        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id_PhatDo")]

        public int Id_PhatDo { get; set; }

        [Display(Name = "Id_DoiTuong")]

        public long Id_DoiTuong { get; set; }

        [Display(Name = "Id_NhomBenh")]

        public int Id_NhomBenh { get; set; }

        [Display(Name ="TenBenh")]
        public string TenBenh { get; set; }

        [Display(Name = "MuiTiem")]

        public string MuiTiem { get; set; }

        [Display(Name = "DoTuoi")]

        public int DoTuoi { get; set; }

        [Display(Name = "DonViTinh")]

        public string DonViTinh { get; set; }

        [Display(Name = "ViTriTiem")]

        public string ViTriTiem { get; set; }

        [Display(Name ="ThoiGianTiem")]
        public string ThoiGianTiem { get; set; }

        [Display(Name = "Status")]

        public string Status { get; set; }

        [Display(Name = "SeoSauTiem")]

        public string SeoSauTiem { get; set; }

        [Display(Name = "DiaDiemTiem")]

        public string DiaDiemTiem { get; set; }

        [Display(Name = "NgayTiem")]

        public DateTime NgayTiem { get; set; }

        [Display(Name = "GhiChu")]

        public string GhiChu { get; set; }

        [Display(Name = "Chitiet")]

        public string Chitiet { get; set; }

        [Display(Name = "TCMR")]

        public bool TCMR { get; set; }

        [Display(Name = "Properties")]

        public string Properties { get; set; }

        [Display(Name = "CreatedDate")]

        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedByUser")]

        public string CreatedByUser { get; set; }

        [Display(Name = "ModifiedDate")]

        public DateTime ModifiedDate { get; set; }

        [Display(Name = "ModifiedByUser")]

        public string ModifiedByUser { get; set; }
        #endregion
    }
}
