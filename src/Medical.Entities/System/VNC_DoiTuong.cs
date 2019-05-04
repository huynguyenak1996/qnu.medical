using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Medical.Entities.System
{
    #region #================ Class Model VNC_DoiTuong ====================#
    /// <summary>
    /// This object represents the properties and methods of a VNC_DoiTuong.
    /// </summary>
    public class VNC_DoiTuong
    {

        #region Public Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "MaSoDT")]

        public string MaSoDT { get; set; }

        [Display(Name = "HoVaTen")]

        public string HoVaTen { get; set; }

        [Display(Name = "NgaySinh")]

        public DateTime NgaySinh { get; set; }

        [Display(Name = "DienThoai")]

        public string DienThoai { get; set; }

        [Display(Name = "CMND")]

        public string CMND { get; set; }

        [Display(Name = "GioiTinh")]

        public string GioiTinh { get; set; }

        [Display(Name = "DanToc")]

        public string DanToc { get; set; }

        [Display(Name = "DiaChiDK")]

        public string DiaChiDK { get; set; }

        [Display(Name = "XaPhuongDK")]

        public string XaPhuongDK { get; set; }

        [Display(Name = "QuanHuyenDK")]

        public string QuanHuyenDK { get; set; }

        [Display(Name = "TinhTPDK")]

        public string TinhTPDK { get; set; }

        [Display(Name = "DiaChiHK")]

        public string DiaChiHK { get; set; }

        [Display(Name = "XaPhuongHK")]

        public string XaPhuongHK { get; set; }

        [Display(Name = "QuanHuyenHK")]

        public string QuanHuyenHK { get; set; }

        [Display(Name = "TinhTPHK")]

        public string TinhTPHK { get; set; }

        [Display(Name = "HoTenCha")]

        public string HoTenCha { get; set; }

        [Display(Name = "NamSinhCha")]

        public string NamSinhCha { get; set; }

        [Display(Name = "DienThoaiCha")]

        public string DienThoaiCha { get; set; }

        [Display(Name = "CMNDCha")]

        public string CMNDCha { get; set; }

        [Display(Name = "HoTenMe")]

        public string HoTenMe { get; set; }

        [Display(Name = "NamSinhMe")]

        public string NamSinhMe { get; set; }

        [Display(Name = "DienThoaiMe")]

        public string DienThoaiMe { get; set; }

        [Display(Name = "CMNDMe")]

        public string CMNDMe { get; set; }

        [Display(Name = "HoTenGiamHo")]

        public string HoTenGiamHo { get; set; }

        [Display(Name = "NamSinhGiamHo")]

        public string NamSinhGiamHo { get; set; }

        [Display(Name = "DienThoaiGiamHo")]

        public string DienThoaiGiamHo { get; set; }

        [Display(Name = "CMNDGiamHo")]

        public string CMNDGiamHo { get; set; }

        [Display(Name = "Properties")]

        public string Properties { get; set; }

        [Display(Name = "Status")]

        public string Status { get; set; }

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
    #endregion
}

