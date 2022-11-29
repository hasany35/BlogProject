using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject_5175.WEB.Views.Shared.Components.Articles
{


    [ViewComponent(Name = "Articles")]
    public class ArticlesViewComponent : ViewComponent
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ArticlesViewComponent(IArticleRepository articleRepository, ICategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            List<GetArticleWithUserVM> list = _articleRepository.GetByDefaults
                (
                    selector: a => new GetArticleWithUserVM()
                    {
                        Title = a.Title,
                        Content = a.Content,
                        CreatedDate = a.CreateDate,
                        UserId = a.AppUserID,
                        UserFullName = a.AppUser.FullName,
                        Image = a.Image,
                        ArticleId = a.ID,
                        CategoryName = a.Category.Name,
                        
                    },
                    expression: a => a.Statu != Statu.Passive && a.Confirm == AdminConfirm.Confirmed,
                    include: a => a.Include(a => a.AppUser).Include(a => a.Category),
                    orderby: a => a.OrderByDescending(a => a.CreateDate)
                ).Take(5).ToList();
            ViewBag.AllCategories = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);
            return View(list);
        }
    }
}
