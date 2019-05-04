using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Medical.Entities.System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Medical.Web.Controllers
{
    public class RegisterController : Controller
    {
        private string apiUrl = "http://localhost:8088/api/";
        private static bool isEmailRegister = false;
        //UserRegisterModel userRegisterModel = new UserRegisterModel();
        public ActionResult Index(UserRegisterModel userRegisterModel = null)
        {
            return View(userRegisterModel);
        }

        [HttpPost]
        public async Task<JsonResult> SendCodeValidate([FromBody] string emailOrPhoneNumber)
        {
            if(new EmailAddressAttribute().IsValid(emailOrPhoneNumber))
            {
                isEmailRegister = true;
                int count = 1;
                var emailAddress = "email=" + emailOrPhoneNumber;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                sendRequest:
                    HttpResponseMessage response = await client.PostAsync("register/sendEmailValidate?" + emailAddress, null);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return new JsonResult(true);
                        }
                        else
                        {
                            if (count <= 3)
                            {
                                count++;
                                goto sendRequest;
                            }
                            else
                            {
                                ViewData["StatusMessage"] = "false";
                                return new JsonResult(false);
                            }
                        }
                    }
                }
            }
            else
            {
                isEmailRegister = false;
                int count = 1;
                var phoneNumber = "phoneNumber=" + emailOrPhoneNumber;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                sendRequest:
                    HttpResponseMessage response = await client.PostAsync("register/sendPhoneNumberValidate?" + phoneNumber, null);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return new JsonResult(true);
                        }
                        else
                        {
                            if (count <= 3)
                            {
                                count++;
                                goto sendRequest;
                            }
                            else
                            {
                                ViewData["StatusMessage"] = "false";
                                return new JsonResult(false);
                            }
                        }
                    }
                }
            }
            return new JsonResult(false);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateCode( string code)
            {
            code = "code=" + code;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("register/validateCode?" + code);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            var dataRespont = JsonConvert.DeserializeObject(data).ToString();
                            UserRegisterModel userRegisterModel = new UserRegisterModel();
                            if (isEmailRegister)
                            {
                                userRegisterModel.Email = JsonConvert.DeserializeObject(data).ToString();
                                userRegisterModel.UserName = userRegisterModel.Email;
                                return View("Index", userRegisterModel);
                            }
                            else
                            {
                                userRegisterModel.PhoneNumber = JsonConvert.DeserializeObject(data).ToString();
                            userRegisterModel.UserName = userRegisterModel.PhoneNumber;
                            return View("Index", userRegisterModel);
                            }
                        }
                    }
            }
            return View();
            }

        [HttpPost]
        public async Task<ActionResult> Create(UserRegisterModel userRegisterModel)
        {
            var a = isEmailRegister;
            string requestUri = apiUrl+ "userManager/createUser";
            if (ModelState.IsValid)
            {
                userRegisterModel.LockoutEnd = DateTime.Now;
                string payLoad = JsonConvert.SerializeObject(userRegisterModel);
                using (HttpClient client = new HttpClient())
                {
                    var httpContent = new StringContent(payLoad, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(requestUri, httpContent))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // chuyển qua trang đăng nhập và ok
                        }
                        else
                        {
                            // báo lỗi
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
     }
}