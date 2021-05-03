using projectsleemwebapp.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.IServices
{
   public interface IpostsServices
    {
        public  Task postsnotshow();
        public  Task<List<Posts>> getposts_all( string name);
        public  Task<Posts> getpostsbyid( int id);
       
    }
}
