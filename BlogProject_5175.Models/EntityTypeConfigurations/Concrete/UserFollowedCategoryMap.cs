using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.Models.EntityTypeConfigurations.Concrete
{
    public class UserFollowedCategoryMap : IEntityTypeConfiguration<UserFollowedCategory>
    {
        public void Configure(EntityTypeBuilder<UserFollowedCategory> builder)
        {
            builder.HasKey(a => new { a.AppUserID, a.CategoryID });
            //builder.HasOne(a => a.AppUser).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.AppUserID);
            //builder.HasOne(a => a.Category).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.CategoryID);
        }
    }
}
