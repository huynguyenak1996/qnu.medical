using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical.Data;
using Medical.Entities.System;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CheckMemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("checkPhoneNumber")]
        public ActionResult CheckPhoneNumber(string phoneNumber)
        {
            var a= _context.VNC_DoiTuong.Where(t => t.DienThoaiCha == phoneNumber || t.DienThoaiMe == phoneNumber || t.DienThoaiGiamHo==phoneNumber).ToList();
            if (a.Count >0)
            {
                List<long> maSoDT=new List<long>();
                foreach(var item in a)
                {
                    maSoDT.Add(item.Id);
                }
                return Ok(maSoDT);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("addMember")]
        public async Task<ActionResult> AddMember(List<string> DoiTuongId, string userId)
        {
            try
            {
                foreach (var item in DoiTuongId)
                {
                    await _context.VNC_ThanhVien.AddAsync(new VNC_ThanhVien()
                    {
                        UserID = userId,
                        DoiTuongID = Convert.ToInt32(item)
                    });

                }
                await _context.SaveChangesAsync();
                return Ok("Đã thêm thành công");
            }
            catch
            {
                return BadRequest("Lỗi trong quá trình thêm thành viên!");
            }
            
        }
    } 
}