using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical.Data;
using Medical.Entities.Model;
using Medical.Entities.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoTiemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SoTiemController(AppDbContext context)
        {
            _context = context;
        }

        private string ReadDoTuoi(int num) {

            string strRef = "{0}";
            int t = num % 12;
            int c = num / 12;
            if (num == 0) { strRef = "Sơ sinh"; }
            if (c > 0) {
                strRef = string.Format(strRef, c) + " tuổi {0}";
            }
            if (t > 0)
            {
                strRef = string.Format(strRef, t) + " tháng";
            }
            else {
                strRef = string.Format(strRef, t) + "";
            }
            return strRef;
        } 
        [HttpGet]
        [Route("duLieuSotiem")]
        public IActionResult GetSoTiem(string DoiTuongID)
        {
            try
            {
                long IdTemp;
                var result = _context.VNC_PhacDoDoiTuong
                    .Where(t => t.Id_DoiTuong == (long.TryParse(DoiTuongID, out IdTemp) ? long.Parse(DoiTuongID) : 0))
                    .Select(x => new SoTiem
                    {
                        Id = x.Id,
                        Id_PhatDo = x.Id_PhatDo,
                        Id_DoiTuong = x.Id_DoiTuong,
                        Id_NhomBenh = x.Id_NhomBenh,
                        MuiTiem = x.MuiTiem,
                        DoTuoi = x.DoTuoi,
                        DonViTinh = x.DonViTinh,
                        ViTriTiem = x.ViTriTiem,
                        Status = x.Status,
                        SeoSauTiem = x.SeoSauTiem,
                        DiaDiemTiem = x.DiaDiemTiem,
                        NgayTiem = Convert.ToDateTime(x.NgayTiem),
                        ThoiGianTiem = ReadDoTuoi(x.DoTuoi),
                        GhiChu = x.GhiChu,
                        Chitiet = x.Chitiet,
                        TCMR = x.TCMR,
                        Properties = x.Properties

                    });
                return Ok(result);
            }
            catch
            {
                return BadRequest("Lỗi khi lấy dữ liệu!");
            }
        }
    }
}