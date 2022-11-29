using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Abstract;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
   public class CategoryRepository :BaseRepository<Category>,ICategoryRepository
    {
        //ProjectContext _context;   // saglıksız
        public CategoryRepository(ProjectContext context):base(context)
        {
            //_context= context;  // saglıksız
        }
        /*
         kullanıcının takip ettiği kategorileri alan componentte kullanacagımız metot
        bahsedilen metot UserFollowedCategory ViewComponent !
         */
        public List<Category> GetCategoryWithUser(int id)
        {
            return context.FollowedCategories.Include(a=>a.AppUser).Include(a=>a.Category)
                .Where(a=>a.AppUserID==id).Select(a=>a.Category).ToList();
        }
    }
}
