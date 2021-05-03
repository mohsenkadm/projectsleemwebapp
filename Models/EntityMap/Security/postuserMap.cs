using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectsleemwebapp.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.EntityMap.Security
{
    public class postuserMap : IEntityTypeConfiguration<postuser>
    {

        public void Configure(EntityTypeBuilder<postuser> builder)
        {
            builder.ToTable("postuser", "dbo");
            builder.HasKey(x => x.postuser_id);
            builder.Property(x => x.User_id);
            builder.Property(x => x.post_id);
            builder.Property(x => x.price_item);
            builder.Property(x => x.data_insert);
            builder.Ignore(x => x.username);
        }
    }
}
