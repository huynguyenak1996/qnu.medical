using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Medical.API.Services;
using Medical.Data;
using Medical.Entities.Model;
using Medical.Entities.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    { 
        // Document reference
        //https://medium.com/@kevinrodrguez/enabling-email-verification-in-asp-net-core-identity-ui-2-1-b87f028a97e0

        private static volatile ConcurrentDictionary<string, InforRegister> _concurrentDictionary = new ConcurrentDictionary<string, InforRegister>();
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterController(IEmailSender emailSender, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _context = context;
            _userManager = userManager;
        }

        // POST api/SendEmailValidate
        [HttpPost]
        [Route("sendEmailValidate")]
        public async Task<IActionResult> SendEmailValidate(string email)
        {
            var checkEmailExits = await _userManager.FindByEmailAsync(email);
            if (checkEmailExits != null)
            {
                return BadRequest("Email đã tồn tại!");
            }
            else
            {
                Random r = new Random();
                var code = r.Next(100000, 999999);
                try
                {
                    InforRegister emailRegister = new InforRegister();
                    emailRegister.EmailOrPhoneNumber = email;
                    emailRegister.CodeValidate = code.ToString();
                    emailRegister.Expried = DateTime.Now.AddMinutes(5);

                    await _emailSender.SendEmailAsync(email, "Xác nhận tai khoản đăng ký",
                            $"Please confirm your account by  code :<label>'{HtmlEncoder.Default.Encode(code.ToString())}' .Comeback and enter code</label>.");
                    _concurrentDictionary[code.ToString()] = emailRegister;
                    return Ok("Đã gửi mã xác nhận đến địa chỉ mail "+email);
                }
                catch
                {
                    return BadRequest("Không thể gửi mã xác nhận!");
                }
            }
        }
        // POST api/SendPhoneNumberValidate
        [HttpPost]
        [Route("sendPhoneNumberValidate")]
        public ActionResult SendPhoneNumberValidate(string phoneNumber)
        {
            //var checkPhoneNumberExits =  _context.VNC_DoiTuong.Where(s=>s.DienThoaiMe==phoneNumber || s.DienThoaiCha==phoneNumber).FirstOrDefault();
            //if (checkPhoneNumberExits != null)
            //{ 
            //    return BadRequest("Số điện thoại đăng ký đã tồn tại trong hệ thống!");
            //}
            //else
            //{
                try
                {
                    InforRegister inforRegister = new InforRegister();
                    inforRegister.EmailOrPhoneNumber = phoneNumber;
                    inforRegister.CodeValidate="123456";
                    inforRegister.Expried = DateTime.Now.AddMinutes(1);
                    _concurrentDictionary["123456"] = inforRegister;
                    return Ok("Đã gửi mã xác nhận đến sdt:  " + phoneNumber);
                }
                catch
                {
                    return BadRequest("Lỗi trong khi gữi mã xác nhận đến sdt!");
                }
            //}
        }


        [HttpGet]
        [Route("validateCode")]
        public ActionResult ValidateCode(string code)
        {
            if (_concurrentDictionary.ContainsKey(code))
            {
                var Expried = _concurrentDictionary[code];
                var result = DateTime.Compare(Convert.ToDateTime(Expried.Expried), DateTime.Now);
                if (result > 0)
                {
                    var data = _concurrentDictionary.Where(t => t.Key.Contains(code)).FirstOrDefault();
                    return Ok(data.Value.EmailOrPhoneNumber);
                }
                else
                {
                    // xóa email và phải nhập lại email để gửi mã;
                    _concurrentDictionary.TryRemove(code, out InforRegister emailOut);

                    // lm sạch cái email rác trong bộ nhớ
                    foreach (var item in _concurrentDictionary.Values)
                    {
                        var expried = DateTime.Compare(Convert.ToDateTime(item.Expried), DateTime.Now);
                        if (expried < 0)
                        {
                            _concurrentDictionary.TryRemove(item.CodeValidate, out InforRegister emailDelete);
                        }
                    }
                    return BadRequest("Mã xác thực đã hết hạn!");
                }
            }
             return BadRequest("Mã xác thực không đúng!");
        }
    }
}
