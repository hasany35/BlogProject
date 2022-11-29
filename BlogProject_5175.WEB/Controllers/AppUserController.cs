using AutoMapper;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Models.DTOs;
using BlogProject_5175.WEB.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;


        public AppUserController(IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager, IMapper mapper, IArticleRepository articleRepository, ICategoryRepository categoryRepository)
        {
            this.appUserRepository = appUserRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO createUserDTO)
        {
            if (ModelState.IsValid) 
            {


                bool userNameCheck = userManager.Users.Any(a => a.Email == createUserDTO.Mail);

                bool userMailCheck = userManager.Users.Any(a => a.UserName== createUserDTO.Mail);

                if (!userNameCheck && !userMailCheck )
                {
                    string newId = Guid.NewGuid().ToString();
                    IdentityUser ıdentityUser = new IdentityUser { Email = createUserDTO.Mail, UserName = createUserDTO.UserName, Id = newId };
                    IdentityResult result = await userManager.CreateAsync(ıdentityUser, createUserDTO.Password);

                    if (result.Succeeded) 
                    {
                        await userManager.AddToRoleAsync(ıdentityUser, "Member");   
                        var user = mapper.Map<AppUser>(createUserDTO);
                        user.IdentityId = newId;


                        using var image = Image.Load(createUserDTO.ImagePath.OpenReadStream());
                        image.Mutate(a => a.Resize(100, 100));  // mutate :  değiştirmek demek aslın / fotoğraf yeniden şekilleniyor
                        image.Save($"wwwroot/images/{user.UserName}.jpg"); // root altına foto kaydı için dosya açtık ve yol verdik.
                        user.Image = ($"/images/{user.UserName}.jpg"); // veritabanına dosya yolunu kaydediyor
                        user.Confirm = BlogProject_5175.Models.Enums.AdminConfirm.Waiting;
                        appUserRepository.Create(user);
                        return RedirectToAction("Login", "Home");  // ilk kez kkayıt olan kulllanıcıyı login sayfasına yönlendir.
                    }
                }
            }
            
            TempData["Message"] = "bir hata oluştu tekrar deneyiniz.. =)";

            return View(createUserDTO);
        }

        public async Task<IActionResult> Detail(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.ID == id);
            IdentityUser identityUser = await userManager.FindByIdAsync(appUser.IdentityId);

            List<Article> articleList = articleRepository.GetDefaults(a => a.Statu != Statu.Passive && a.AppUserID == appUser.ID);

            List<Category> userFollowedCategories = categoryRepository.GetCategoryWithUser(appUser.ID);

            GetProfileVM getProfileVM = new GetProfileVM()
            {
                FullName = appUser.FullName,
                Image = appUser.Image,
                Mail = identityUser.Email,
                Articles = articleList,
                Categories = userFollowedCategories,
            };

            foreach (var item in getProfileVM.Articles)
            {
                item.Category = categoryRepository.GetDefault(a => a.Statu != Statu.Passive && a.ID == item.CategoryID);
            }

            return View(getProfileVM);
        }
    }
}
