using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using projectsleemwebapp.Classes;
using projectsleemwebapp.Models;
using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.IServices;

namespace projectsleemwebapp.Controllers
{
    public class AccountController : MasterController
    {
        private readonly IUserServices _userServices;
        
        public AccountController(PblogsContext context,IUserServices userServices, IMemoryCache cache)
        {
            _context = context;
            _userServices = userServices;
            _Cache = cache;
        }
        [AllowAnonymous]
        [Route("Account/login1")]
        public IActionResult SignIn()
        {
           /// this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }
    
       
        public IActionResult SignUp()
        {
            return View();
        }  
        public IActionResult usersforadmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(string username,string password)
        {
            try
            {
                //daaper
                Users user = await _userServices.Login_chek(username, password);
                if (user == null)
                { return Json(new { success = false, msg = "عذرا اسم المستخدم او كلمة المرور خطا" }); }
                user.Isonline = true;
                user.Lastlogin = DateTime.UtcNow;
                user.Token = new JsonWebToken().GenerateToken(user.Id, username);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _UserId = user.Id;
                UserManger = user;
                HttpContext.Session.SetString("isadmin", user.isadmin.ToString());

                return Json(new { success = true, data = user.Token, msg = "تم تسجيل االدخول بنجاح" });
            }
            catch (Exception ex)
            {
              
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الدخول" });
            }
           
         }
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Register")]
        public async Task<JsonResult> Register(Users users)
        {
        
            try
            {
                Users check = await _userServices.CheckUserinfo(users.UserName, users.Email);
                if (check != null)
                    return Json(new { success = false, msg = "عذرا اسم المتسخدم او البريد الالكتروني  محجوز" });
                Random random = new Random();
                users.Isonline = false;
                users.IsActive = true;
                users.IsDeletet = false;
                users.InsertDate = DateTime.UtcNow;
                users.IsConfirm = false;
                users.isadmin = false;
                users.Code = random.Next(100000, 999999).ToString();
               // MailService mail = new MailService();
               // mail.SendMail(users.Email, "car store", "Confirm Account Code : " + users.Code);

                await _context.Users.AddAsync(users);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم انشاء الحساب  بنجاح  يرجى متابعه البريد الالكتروني لتاكيده" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }
         
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/ForgatePassword")]
        public async Task<JsonResult> ForgatePassword(string email) 
        {
            try
            {
                Users check = await _userServices.CheckUserinfo("", email);
                if (check == null)
                    return Json(new { success = false, msg = "عذرا بريد الالكتروني غير موجود سابقا" });
                Random random = new Random();
                check.Code = random.Next(100000, 999999).ToString();
                MailService mail = new MailService();
                mail.SendMail(check.Email, "car store", "Confirm Account Code : " + check.Code);

                _context.Entry(check).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم ارسال الرمز   بنجاح  يرجى متابعه البريد الالكتروني لتاكيده" });
            }
            catch (Exception ex)
            {
            
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تعديل الحساب" });
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Update_Pass")]
        public async Task<JsonResult> Update_Pass(string pass,string email)
        {
            try
            {
                Users check = await _userServices.CheckUserinfo("", email);
                if (check == null)
                    return Json(new { success = false, msg = "عذرا بريد الالكتروني غير موجود سابقا" });
                check.Password = pass;
                _context.Entry(check).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم تعديل كلمة المرور بنجاح" });
            }
            catch (Exception ex)
            {
               
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تعديل كللمة المرور" });
            }

        }
        [HttpPost]
        public async Task<JsonResult> ConfirmAccount(string code, string username, string Email)
        {
            try
            {
                Users users = await _userServices.checkConfirmAccount(code, username, Email);
                if (users == null)
                    return Json(new { success = false, msg = "معلومات التاكيد غير صحيحه" });
                users.IsConfirm = true;
                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم تاكيد الحساب بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تعديل  الحساب" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> Getuser(string user_name)
        {
            try
            {
                List<Users> users = await _userServices.getuser_all(user_name);
              
                return Json(new { success = true ,data=users });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب معلومات" });
            }
        }
        [HttpGet]
     //   [Authorize]
        public async Task<JsonResult> Logout()
        {
            try
            {
                Users users = await _userServices.getbyid(UserManger.Id);
                if (users == null)
                    return Json(new { success = false, msg = "عمليه تسجيل الدخول لم تكتمل" });

                users.Isonline = false;
                users.Lastlogout = DateTime.UtcNow;

                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                UserManger = null;
                HttpContext.Session.SetString("isadmin", "false");
                return Json(new { success = true, msg = "تم تسجيل الخروج بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الخروج" });
            }
        }
        [HttpDelete]
        //   [Authorize]
        public async Task<object> Deleteuser(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("isadmin") == "True")
                {
                    var t = _context.Users.Find(id);
                    _context.Users.Remove(t);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, msg = "تم حذف السمتخدم بنجاح" });
                }
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "حدث خطا اثناء عملية حذف المستخدم" });
            }
        }
        
    }

}