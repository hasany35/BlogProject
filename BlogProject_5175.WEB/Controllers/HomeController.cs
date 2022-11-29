using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Models;
using BlogProject_5175.WEB.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;

        public HomeController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser =await userManager.FindByEmailAsync(dto.Mail);
                if (identityUser!=null )  
                {
                    AppUser user = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
                    if (user.Statu != Statu.Passive)
                    {


                        await signInManager.SignOutAsync(); 
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(identityUser, dto.Password, true, true);

                        if (result.Succeeded)
                        {
                            string role = (await userManager.GetRolesAsync(identityUser)).FirstOrDefault();
                            return RedirectToAction("Index", "AppUser", new { area = role });
                        }
                    }
                }
            }
            TempData["Message"] = "Mail adresi veya şifreniz yanlış!! ";
            return View(dto);
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
