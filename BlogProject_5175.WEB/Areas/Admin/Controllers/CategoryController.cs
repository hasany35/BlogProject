using AutoMapper;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Admin.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserFollowedCategoryRepository _userFollowedCategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository, IUserFollowedCategoryRepository userFollowedCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _userFollowedCategoryRepository = userFollowedCategoryRepository;
        }

        public async Task<IActionResult> List()
        {

            List<Category> model = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);

            return View(model);
        }

        public IActionResult Update(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);

            var updateCategory = _mapper.Map<UpdateCategoryDTO>(category);
            return View(updateCategory);
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(updateCategoryDTO);
                _categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(updateCategoryDTO);
        }

        public IActionResult CheckList()
        {
            List<Category> categories = _categoryRepository.GetDefaults(a => a.Statu == Statu.Passive && a.Confirm == AdminConfirm.Waiting);
            return View(categories);
        }

        public IActionResult Approve(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            _categoryRepository.Confirmed(category);
            return RedirectToAction("Checklist");
        }

        public IActionResult Reject(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            _categoryRepository.Rejected(category);
            return RedirectToAction("Checklist");
        }
        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            _categoryRepository.Delete(category);
            return RedirectToAction("List");
        }
    }
}
