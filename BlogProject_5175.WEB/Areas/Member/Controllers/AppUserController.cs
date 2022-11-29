using AutoMapper;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Member.Models.DTOs;
using BlogProject_5175.WEB.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class AppUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserFollowedCategoryRepository _userFollowedCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AppUserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAppUserRepository appUserRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment, IArticleRepository articleRepository,
            IUserFollowedCategoryRepository userFollowedCategoryRepository,
            ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _articleRepository = articleRepository;
            _userFollowedCategoryRepository = userFollowedCategoryRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            AppUser user = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            if (user != null)
            {
                return View(user);
            }

            return Redirect("~/");                     // areasız başlangıç sayfasına yani home/index
                                                       // return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");       // areasız başlangıç sayfasına yani home/index
        }


        public async Task<IActionResult> Update()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            AppUser user = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            ;


            UpdateAppUserDTO updateAppUserDTO = _mapper.Map<UpdateAppUserDTO>(user);
            updateAppUserDTO.Mail = identityUser.Email;
            updateAppUserDTO.oldPassword = user.Password;
            updateAppUserDTO.oldImage = user.UserName;
            ;

            return View(updateAppUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAppUserDTO dto)
        {

            if (ModelState.IsValid)
            {
                string oldPassword = _appUserRepository.GetDefault(a => a.ID == dto.ID).Password;
                string oldPassword2 = _appUserRepository.GetDefault(a => a.ID == dto.ID).Password2;
                string oldPassword3 = _appUserRepository.GetDefault(a => a.ID == dto.ID).Password3;
                if (dto.Password != oldPassword && dto.Password != oldPassword2 && dto.Password != oldPassword3)
                {

                    string oldImage = _appUserRepository.GetDefault(a => a.ID == dto.ID).UserName;
                    AppUser appUser = new AppUser();
                    appUser.Statu = Statu.Modified;
                    appUser.FirstName = dto.FirstName;
                    appUser.LastName = dto.LastName;
                    appUser.UserName = dto.UserName;
                    appUser.Password = dto.Password;
                    appUser.Password2 = oldPassword;
                    appUser.Password3 = oldPassword2;
                    appUser.Image = dto.Image;
                    appUser.ImagePath=dto.ImagePath;

                    IdentityUser identityUser = await _userManager.FindByIdAsync(dto.IdentityId);

                    if (identityUser != null)
                    {
                        identityUser.Email = dto.Mail;
                        identityUser.UserName = appUser.UserName;

                        await _userManager.ChangePasswordAsync(identityUser, oldPassword, appUser.Password);
                        IdentityResult result = await _userManager.UpdateAsync(identityUser);

                        if (result.Succeeded)
                        {
                            if (appUser.ImagePath != null)
                            {
                                string imageName = oldImage + ".jpg";

                                string deletedImage = Path.Combine(_webHostEnvironment.WebRootPath, "images", $"{imageName}");

                                if (System.IO.File.Exists(deletedImage))
                                {
                                    System.IO.File.Delete(deletedImage);
                                }
                                using var image = Image.Load(dto.ImagePath.OpenReadStream());

                                image.Mutate(a => a.Resize(1000, 1000));

                                image.Save($"wwwroot/images/{appUser.UserName}.jpg");
                                appUser.Image = ($"/images/{appUser.UserName}.jpg");
                            }
                            ;
                            _appUserRepository.Update(appUser);
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Girdiğiniz şifre önceki 3 şifrenizden farklı olmalıdır";
                }
            }




            return View(dto);
        }

        public async Task<IActionResult> Delete()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            AppUser user = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            _appUserRepository.Delete(user);

            return Redirect("~/");       // areasız başlangıç sayfasına yani home/index

        }

        public async Task<IActionResult> Detail(int id)
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);


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

            // Eager loading olduğu için GetProfileVM içerisindeki List<Articles> listesinin categort elemanları foreach ile doldurulur
            foreach (var item in getProfileVM.Articles)
            {
                item.Category = _categoryRepository.GetDefault(a => a.Statu != Statu.Passive && a.ID == item.CategoryID);
            }


            return View(getProfileVM);
        }
    }
}
