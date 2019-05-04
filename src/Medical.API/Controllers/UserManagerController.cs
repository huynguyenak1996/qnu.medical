using Medical.Data;
using Medical.Entities.Model;
using Medical.Entities.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public UserManagerController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,AppDbContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // POST api/Create  New User
        [HttpPost]
        [Route("createUser")]
        public async Task<ActionResult> CreateUser(UserRegisterModel userRegister)
        {
            var user = new ApplicationUser { UserName = userRegister.UserName, Email = userRegister.Email, FirstName = userRegister.FirstName, LastName = userRegister.LastName, PhoneNumber = userRegister.PhoneNumber, LockoutEnd = userRegister.LockoutEnd, Gender = userRegister.Gender };
            var resultAddUser = await _userManager.CreateAsync(user, userRegister.Password);
            if (resultAddUser.Succeeded)
            {
                var tmpUser = await _userManager.FindByNameAsync(userRegister.UserName);
                var roleUser = _context.ApplicationRoles.Where(x => x.Name.Contains("ClientUser")).FirstOrDefault();
                if (roleUser != null)
                {
                    await _context.ApplicationUserRole.AddAsync(new ApplicationUserRole { RoleId = roleUser.Id, UserId = tmpUser.Id });
                    await _context.UserProfiles.AddAsync(new Entities.Model.UserProfile
                    {
                        UserId = tmpUser.Id,
                        FirstName = userRegister.FirstName,
                        LastName = userRegister.LastName,
                        Address = userRegister.Address,
                        // Birthday = DateTime
                        // Avatar=
                    });

                    await _context.SaveChangesAsync();

                    return Ok("Đã tạo tài khoản");
                }
            }
            else
            {
                return BadRequest("Tạo mới tài khoản thất bại!");
            }
            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserManager user)
        {
           var result= await _signInManager.PasswordSignInAsync(user.UserName,user.Password,false,false);
            if (result.Succeeded)
            {
                //return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(true));
                var _user=  await _userManager.FindByNameAsync(user.UserName);
                Entities.Model.UserProfile userProfiles = _context.UserProfiles.Where(t => t.UserId == _user.Id).Select(t => new Entities.Model.UserProfile
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    UserName = user.UserName,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Address = t.Address,
                    Grade = t.Grade,
                    Birthday = t.Birthday,
                    Avatar = t.Avatar,
                    LastLoginIP = t.LastLoginIP,
                    SignInIP = t.SignInIP,
                    LastLoginDevice = t.LastLoginDevice,
                    Properties=t.Properties,
                    CreatedDate=t.CreatedDate,
                    CreatedByUser=t.CreatedByUser,
                    ModifiedDate=t.ModifiedDate,
                    ModifiedByUser=t.ModifiedByUser
                }).FirstOrDefault();
          
                return Ok(userProfiles);
            }
            else
            {
                return BadRequest(false);
            }

        }
  
    }
}