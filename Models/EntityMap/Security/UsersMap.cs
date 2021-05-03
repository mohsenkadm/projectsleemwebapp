using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectsleemwebapp.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.EntityMap.Security
{
    public class UsersMap: IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName);
            builder.Property(x => x.Password);
            builder.Property(x => x.Email);
            builder.Property(x => x.Code);
            builder.Property(x => x.IsConfirm);
            builder.Property(x => x.Isonline);
            builder.Property(x => x.Lastlogin);
            builder.Property(x => x.Lastlogout);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.InsertDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.IsDeletet);
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.isadmin);
            builder.Ignore(x => x.Token);
        }
    }
}
