using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Services
{
    public class postsServices : IpostsServices
    {
        private readonly MasterService<Posts> _postService;
        public postsServices()
        {
            _postService = new MasterService<Posts>();
        }

        public async Task postsnotshow()
        {
           await _postService.RunSpAsync("dbo.postsnotshow",new { });
        }  
        public async Task<List<Posts>> getposts_all(string name)
        {
         return  await _postService.GetEntityListAsync("dbo.getposts_all", new { name });
        }
        public async Task<Posts> getpostsbyid(int id)
        {
         return  await _postService.GetEntityAsync("dbo.getpostsbyid", new { id });
        }
     
    }
}
