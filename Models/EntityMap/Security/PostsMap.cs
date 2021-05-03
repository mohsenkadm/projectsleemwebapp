using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectsleemwebapp.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.EntityMap.Security
{
    public class PostsMap : IEntityTypeConfiguration<Posts>
    {

        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.ToTable("Posts", "dbo");
            builder.HasKey(x => x.post_id);
            builder.Property(x => x.Item_name);
            builder.Property(x => x.Item_dateils);
            builder.Property(x => x.Item_price);
            builder.Property(x => x.item_date);
            builder.Property(x => x.lastuserid);
            builder.Property(x => x.lastprice);
            builder.Property(x => x.image);
            builder.Property(x => x.isshow);
            builder.Ignore(x => x.username);
        }
    }
}
