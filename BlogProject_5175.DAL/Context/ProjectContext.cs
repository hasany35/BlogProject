using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.EntityTypeConfigurations.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.DAL.Context
{
    public class ProjectContext : IdentityDbContext
    {
        public ProjectContext(DbContextOptions options) : base(options) { }


        public DbSet<Article>  Articles { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<AppUser>  AppUsers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment>  Comments { get; set; }
        public DbSet<UserFollowedCategory>  FollowedCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new UserFollowedCategoryMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            base.OnModelCreating(builder);
        }

    }
}
