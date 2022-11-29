using AutoMapper;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserFollowedCategoryRepository _userFollowedCategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, IAppUserRepository appUserRepository,IUserFollowedCategoryRepository userFollowedCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _userFollowedCategoryRepository = userFollowedCategoryRepository;
        }
        [HttpGet]
        public IActionResult Create() => View();
        // Create() {  return View();  }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(dto);
                _categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }

       
        public IActionResult List()
        {
            List<Category> list = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive && a.Confirm==AdminConfirm.Confirmed);
            //
            var userName = this.User.Identity.Name;   // userName => hasan.yilmaz
            AppUser appUser = _appUserRepository.GetDefault(a => a.UserName == userName);
            List<UserFollowedCategory> list2 = _userFollowedCategoryRepository.GetDefaults(a => a.AppUserID == appUser.ID);
            ViewBag.myBag = list2;
            
            return View(list);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            UpdateCategoryDTO updateCategory = _mapper.Map<UpdateCategoryDTO>(category);

            var userName = this.User.Identity.Name;   // userName => hasan.yilmaz
            AppUser appUser = _appUserRepository.GetDefault(a => a.UserName == userName);
            List<UserFollowedCategory> userFollowedCategory2 = category.UserFollowedCategories.Where(a=>a.AppUserID==appUser.ID).ToList();
            // ToD
            List<UserFollowedCategory> userFollowedCategory = category.UserFollowedCategories.Where(a => a.AppUserID == appUser.ID).ToList();
            foreach (var item in userFollowedCategory)
            {
                ViewBag.myFollowedCategory.add(item);
            }
            return View(updateCategory);
        }
        [HttpPost]
        public IActionResult Update(UpdateCategoryDTO dto)
        {
            
            if (ModelState.IsValid)
            {

                Category updatedCategory = _mapper.Map<Category>(dto);                      

                _categoryRepository.Update(updatedCategory);                                

                return RedirectToAction("List");                                            
            }
            return View(dto);
        }


        public IActionResult Follow(int id)
        {
            var userName = this.User.Identity.Name;   // userName => hasan.yilmaz
            AppUser appUser = _appUserRepository.GetDefault(a => a.UserName == userName);   // useri bulduk
            Category category = _categoryRepository.GetDefault(a => a.ID == id);            // kategoriyi bulduk



            UserFollowedCategory followed = _userFollowedCategoryRepository.GetDefault(a => a.AppUserID == appUser.ID && a.CategoryID==id);
            ;
            if (followed!=null)       
            {
                //_userFollowedCategoryRepository.Delete(followed);
            }
            else            
            {
                category.UserFollowedCategories.Add(new UserFollowedCategory
                {
                    Category = category,
                    CategoryID = category.ID,
                    AppUser = appUser,
                    AppUserID = appUser.ID,
                });
                _categoryRepository.Update(category);
            }
            
            return RedirectToAction("List");
        }
        public IActionResult Unfollow(int id)
        {
            var userName = this.User.Identity.Name;   
            AppUser appUser = _appUserRepository.GetDefault(a => a.UserName == userName);   
            Category category = _categoryRepository.GetDefault(a => a.ID == id);            


            UserFollowedCategory followed = _userFollowedCategoryRepository.GetDefault(a => a.AppUserID == appUser.ID && a.CategoryID == id);
            ;
            if (followed != null)       // kategori followlu ise
            {
                _userFollowedCategoryRepository.Delete(followed);
            }

            return RedirectToAction("List");
        }
        
    }
}
