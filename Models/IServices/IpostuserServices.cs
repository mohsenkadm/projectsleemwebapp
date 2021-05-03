using projectsleemwebapp.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.IServices
{
   public interface IpostuserServices
    {
        public  Task<List<postuser>> getpostuser(int id);
    }
}
