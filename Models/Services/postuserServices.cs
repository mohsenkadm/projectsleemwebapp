using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Services
{
    public class postuserServices : IpostuserServices
    {
        private readonly MasterService<postuser> _postuserService;
        public postuserServices()
        {
            _postuserService = new MasterService<postuser>();
        }


        public async Task<List<postuser>> getpostuser(int id)
        {
            return await _postuserService.GetEntityListAsync("dbo.getpostuser", new { id });
        }

    }
}
