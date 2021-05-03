using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Services
{
    public class UserServices : IUserServices
    {
        private readonly MasterService<Users> _userService;
        public UserServices()
        {
            _userService = new MasterService<Users>();
        }

        public async Task<Users> Login_chek(string username, string password)
        {
            return await _userService.GetEntityAsync("dbo.user_login", new { username, password });
        }
        public async Task<Users> CheckUserinfo(string username, string Email)
        {
            return await _userService.GetEntityAsync("dbo.user_CheckUserinfo", new { username, Email });
        }

        public async Task<Users> checkConfirmAccount(string code, string username, string Email)
        {
            return await _userService.GetEntityAsync("dbo.user_checkConfirmAccount", new { code, username, Email });
        }

        public async Task<Users> getbyid(int id)
        {
            return await _userService.GetEntityAsync("dbo.select_user", new { id });
        }

        public List<Users> getusers()
        {
            return  _userService.GetEntityList("dbo.getuserall", null);
        }
        public async Task<Users> GetUserInfo(int Id)
        {
            return await _userService.GetEntityAsync("dbo.getuserinfo", new { Id });
        }

        public async Task<List<Users>> getuser_all(string name)
        {
            return await _userService.GetEntityListAsync("dbo.getuser_all", new {  name });
        }
    }
}
