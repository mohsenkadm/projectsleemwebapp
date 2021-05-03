using Microsoft.EntityFrameworkCore;
using projectsleemwebapp.Models.Entity.Security;
using projectsleemwebapp.Models.EntityMap.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models
{
    public class PblogsContext:DbContext
    {
        public PblogsContext(DbContextOptions<PblogsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new PostsMap());
            modelBuilder.ApplyConfiguration(new postuserMap());
        }
        public  DbSet<Users> Users { get; set; }
        public  DbSet<Posts> posts { get; set; }
        public  DbSet<postuser> postusers { get; set; }

    }
}
