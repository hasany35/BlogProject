using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Concrete;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.WEB.Models.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlogProject_5175.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDbContext<ProjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ProjectContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // baglantı gereksiz gereksiz kopuyor
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>();

            services.ConfigureApplicationCookie(a=> 
            {
                a.LoginPath = new PathString("/Home/Login");
                a.ExpireTimeSpan = TimeSpan.FromDays(1);
                a.Cookie = new CookieBuilder { Name = "KullaniciCookie", SecurePolicy = CookieSecurePolicy.Always };
            });

            services.AddAuthentication(); // kimlik doğrulama

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserFollowedCategoryRepository, UserFollowedCategoryRepository>();

            services.AddAutoMapper(typeof(Mapping));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();    // kimlik doğrulama
            app.UseAuthorization();     // yetkilendirme

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name:"area",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
