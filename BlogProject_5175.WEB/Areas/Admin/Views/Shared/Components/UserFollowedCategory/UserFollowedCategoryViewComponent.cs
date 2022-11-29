using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogProject_5175.WEB.Areas.Admin.Views.Shared.Components.UserFollowCategory
{
    [ViewComponent(Name =("UserFollowedCategory"))]
    public class UserFollowedCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;      

        public UserFollowedCategoryViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke(int id)
        {
            List<Category> followedCategories = _categoryRepository.GetCategoryWithUser(id);
            return View(followedCategories);
        }
    }
}
