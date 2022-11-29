﻿using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    public class AppUserController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AppUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository, IWebHostEnvironment webHostEnvironment, IArticleRepository articleRepository, ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _webHostEnvironment = webHostEnvironment;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<GetArticleWithUserVM> articles = _articleRepository.GetByDefaults
           (
                selector: a => new GetArticleWithUserVM()
                {
                    Title = a.Title,
                    Content = a.Content,
                    UserId = a.AppUser.ID, 
                    UserFullName = a.AppUser.FullName, 
                    UserImage = a.AppUser.Image,
                    ArticleId = a.ID,
                    Image = a.Image,
                    CreatedDate = a.CreateDate,
                    CategoryName = a.Category.Name, 
                    CategoryID = a.CategoryID,
                    ReadingTime = a.ReadingTime,
                    CreateDate = a.CreateDate,
                    ReadCounter = a.ReadCounter
                },
                    expression: a => a.Statu != Statu.Passive,
                    include: a => a.Include(a => a.AppUser).Include(a => a.Category),
                    orderby: a => a.OrderByDescending(a => a.CreateDate)
             );
            ViewBag.AllCategory = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive && a.Confirm == AdminConfirm.Confirmed);
            return View(articles.Take(10).ToList());
        }

        public IActionResult List()
        {
            List<AppUser> appUsers = _appUserRepository.GetDefaults(a => a.Statu != Statu.Passive && a.Confirm==AdminConfirm.Confirmed);
            ViewBag.MailList = _userManager.Users.ToList();
            return View(appUsers);
        }

        public async Task<IActionResult> Delete(int id)
        {
            AppUser appUser = _appUserRepository.GetDefault(a=>a.ID==id);
            IdentityUser identityUser = await _userManager.FindByIdAsync(appUser.IdentityId); ;            
            _appUserRepository.Delete(appUser);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> Detail(int id)
        {

            AppUser appUser = _appUserRepository.GetDefault(a => a.ID == id);
            IdentityUser identityUser = await _userManager.FindByIdAsync(appUser.IdentityId);

            List<Article> articleList = _articleRepository.GetDefaults(a => a.Statu != Statu.Passive && a.AppUserID == appUser.ID);

            List<Category> userFollowedCategories = _categoryRepository.GetCategoryWithUser(appUser.ID);

            GetAppUserProfileVM getProfileVM = new GetAppUserProfileVM()
            {
                FullName = appUser.FullName,
                Image = appUser.Image,
                Mail = identityUser.Email,
                Articles = articleList,
                Categories = userFollowedCategories,
            };

            foreach (var item in getProfileVM.Articles)
            {
                item.Category = _categoryRepository.GetDefault(a => a.Statu != Statu.Passive && a.ID == item.CategoryID);
            }


            return View(getProfileVM);
        }

        public IActionResult Checklist()
        {
            List<AppUser> appUsers = _appUserRepository.GetDefaults(a => a.Confirm==AdminConfirm.Waiting && a.Statu==Statu.Passive);
            ViewBag.MailList = _userManager.Users.ToList().Where(a=>a.Email!=null).ToList();
            return View(appUsers);
        }

        public IActionResult Approve(int id)
        {
            AppUser appUser = _appUserRepository.GetDefault(a=>a.ID== id);
            _appUserRepository.Confirmed(appUser);
            return RedirectToAction("Checklist");
        }

        public IActionResult Reject(int id)
        {
            AppUser appUser = _appUserRepository.GetDefault(a => a.ID == id);
            _appUserRepository.Rejected(appUser);
            return RedirectToAction("Checklist");
        }

    }
}
