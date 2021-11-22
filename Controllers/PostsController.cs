
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;
using projectsleemwebapp.Classes;
using projectsleemwebapp.Models;
using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Controllers
{
    public class PostsController : MasterController
    {
        private readonly IpostsServices _postsServices;
        private readonly IpostuserServices _postuserServices;
        private string FilePath;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostsController(PblogsContext context, IpostsServices postsServices,IpostuserServices postuserServices, IMemoryCache cache, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _postsServices = postsServices;
            _postuserServices = postuserServices;
            _Cache = cache;
            this._hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login1", "Account");
            }
        } 
        public IActionResult postforadmin()
        {
            return View();
        }
        [Route("Posts/post")]
        public IActionResult Posts()
        {
            return View();
        }
        [HttpPost]
        public async Task<object> PostPosts(Posts posts)
        {
            try
            {
                if (posts.image != null)
                {
                    posts.image = posts.image.Substring(12);//posts.image.LastIndexOf("\\"));// _hostEnvironment.WebRootPath + $@"\images\imag_post" + posts.image.Substring(posts.image.LastIndexOf("\\"));
                }
                posts.Item_name = Encyptmethod.EncryptStringToBytes_Aes(posts.Item_name);
                posts.Item_dateils = Encyptmethod.EncryptStringToBytes_Aes(posts.Item_dateils);

                posts.isshow = true;
                await _context.posts.AddAsync(posts);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم انشاء منتج بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء انشاء منتج" });
            }

        }
      [HttpPost]
        public async Task UploadFileAsync()
        {
            IFormFileCollection file = Request.Form.Files;
            try
            {
                FilePath = "";
                long size = 0;

                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Value.Trim('"');
                FilePath = _hostEnvironment.WebRootPath + $@"\images\imag_post\{ filename}";
                size += file[0].Length;
             await   using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }
            }

            catch (Exception ex)
            {
              
            }
        }
        [HttpGet]
        public async Task<JsonResult> setpostuser(postuser postuser)
        {
            try
            {
                postuser.User_id = UserManger.Id;
                postuser.data_insert = DateTime.UtcNow;
                await _context.postusers.AddAsync(postuser);
                await _context.SaveChangesAsync();

                Posts posts = await _postsServices.getpostsbyid(postuser.post_id);

                posts.lastuserid = postuser.User_id;
                posts.lastprice = postuser.price_item;

                _context.Entry(posts).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                postuser postuser1 = new postuser()
                {
                    post_id = postuser.post_id,
                    username = UserManger.UserName,
                    price_item = postuser.price_item
                };

                return Json(new { success = true, data = postuser1, msg = "تم انشاء مزايدة بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء المزايدة" });
            }

        }
        [HttpGet]
        public async Task<JsonResult> Getpost(string item_name)
        {
            try
            {
                await _postsServices.postsnotshow();
                List<Posts> posts = await _postsServices.getposts_all(item_name);
                foreach(var p in posts)
                {
                    p.Item_name = Encyptmethod.DecryptStringFromBytes_Aes(p.Item_name);
                    p.Item_dateils = Encyptmethod.DecryptStringFromBytes_Aes(p.Item_dateils);
                    if(p.username!=null && p.username!="" && p.username!="null")
                    p.username = Encyptmethod.DecryptStringFromBytes_Aes(p.username);

                }
                return Json(new { success = true, data = posts });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب معلومات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> getpostuser(int post_id)
        {
            try
            {
                List<postuser> posts = await _postuserServices.getpostuser(post_id);
                foreach (var p in posts)
                {
                    p.username = Encyptmethod.DecryptStringFromBytes_Aes(p.username);
                }
                return Json(new { success = true, data = posts });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب معلومات" });
            }
        }
        [HttpDelete]
        //   [Authorize]
        public async Task<object> Deletepost(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("isadmin") == "True")
                {
                    var t = _context.posts.Find(id);
                    _context.posts.Remove(t);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, msg = "تم حذف المنتج بنجاح" });
                }
                else return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "حدث خطا اثناء عملية منتج المستخدم" });
            }
        }
    }
}
