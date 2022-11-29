using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_5175.WEB.Areas.Member.Views.Shared.Components.UserFollowedCategory
{
    [ViewComponent(Name = "UserFollowedCategory")]
    public class UserFollowedCategoryComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public UserFollowedCategoryComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke(int id)
        {
            var list = _categoryRepository.GetCategoryWithUser(id);
            
            return View(list);
        }
    }
}
