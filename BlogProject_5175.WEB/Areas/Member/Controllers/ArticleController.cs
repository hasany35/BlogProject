using AutoMapper;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Member.Models.DTOs;
using BlogProject_5175.WEB.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;

        public ArticleController(IArticleRepository articleRepository, IMapper mapper, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository, ICategoryRepository categoryRepository, ILikeRepository likeRepository, ICommentRepository commentRepository)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _categoryRepository = categoryRepository;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
        }
        [HttpGet]     
        public async Task<IActionResult> Create()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);


            CreateArticleDTO dto = new CreateArticleDTO()
            {
                Categories = _categoryRepository.GetByDefaults
                (
                    selector: a => new GetCategoryDTO
                    {
                        ID = a.ID,
                        Name = a.Name
                    },
                    expression: a => a.Statu != Statu.Passive)
                ,
                AppUserID = appUser.ID

            };
            ;
            return View(dto);
        }
        [HttpPost]
        public IActionResult Create(CreateArticleDTO dto)
        {
            ;
            if (ModelState.IsValid)
            {
                var article = _mapper.Map<Article>(dto);

                /*
                 * kabul edilen image uzantıları
                - BMP : BmpDecoder
                - PNG : PngDecoder
                - TGA : TgaDecoder
                - PBM : PbmDecoder
                - JPEG : JpegDecoder
                - Webp : WebpDecoder
                - GIF : GifDecoder
                - TIFF : TiffDecoder
                */
                if (article.ImagePath != null) 
                {
                    using var image = Image.Load(dto.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(500, 500));  // mutate :  değiştirmek demek aslın / fotoğraf yeniden şekilleniyor
                    Guid guidID = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guidID}.jpg"); // root altına foto kaydı için dosya açtık ve yol verdik.
                    article.Image = ($"/images/{guidID}.jpg"); // veritabanına dosya yolunu kaydediyor
                    // buraya kadar fotoyu kaydetme kısmı.

                    _articleRepository.Create(article);
                    return RedirectToAction("List");
                }
            }
            return View();
        }
        public IActionResult List()
        {
            AppUser appUser = _appUserRepository.GetDefault(a => a.UserName == this.User.Identity.Name);
            ;
            var articleList = _articleRepository.GetByDefaults
                (
                    selector: a => new GetArticleVM()
                    {
                        ArticleID = a.ID,
                        CategoryName = a.Category.Name,    
                        Content = a.Content,
                        Image = a.Image,
                        Title = a.Title,
                        UserFullName = a.AppUser.FullName,
                    },
                    expression: a => a.Statu != Statu.Passive && a.AppUserID == appUser.ID && a.Confirm == AdminConfirm.Confirmed,
                    include: a => a.Include(a => a.AppUser).Include(a => a.Category)  
                    //  , orderby: a => a.OrderByDescending(a => a.CreateDate)                // gibi gibi
                );
            ;
            return View(articleList);

        }

        public IActionResult Update(int id)
        {
            Article article = _articleRepository.GetDefault(a => a.ID == id);

            UpdateArticleDTO updateArticle = new UpdateArticleDTO();
            updateArticle.ID = article.ID;
            updateArticle.Title = article.Title;
            updateArticle.Content = article.Content;
            updateArticle.Image = article.Image;
            updateArticle.ImagePath= article.ImagePath;
            updateArticle.CategoryID= article.CategoryID;
            updateArticle.AppUserID= article.AppUserID;


            updateArticle.Categories = _categoryRepository.GetByDefaults
                  (
                      selector: a => new GetCategoryDTO
                      {
                          ID = a.ID,
                          Name = a.Name
                      },
                       expression: a => a.Statu != Statu.Passive && a.Confirm == AdminConfirm.Confirmed
                  );
            return View(updateArticle);
        }
        [HttpPost]
        public IActionResult Update(UpdateArticleDTO dto)   
        {
            if (dto.ImagePath != null)
            {
                if (ModelState.IsValid)
                {
                    Article article = _articleRepository.GetDefault(a => a.ID == dto.ID);
                    string imagepath = article.Image.ToString();
                    var oldImage = article.Image;
                    //
                    article.Title=dto.Title;
                    article.Content=dto.Content;
                    article.ImagePath = dto.ImagePath;
                    article.CategoryID = dto.CategoryID;
                    article.AppUserID = dto.AppUserID;
                    string pathx = @"C:\Users\hasan\Desktop\HASAN YILMAZ BLOG PROJE\BlogProject_5175_unflw\BlogProject_5175.WEB\wwwroot\images\" + article.Image.ToString().Split('/')[2];
                    System.IO.File.Delete(pathx);
                    using var image = Image.Load(dto.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(500, 500));  // mutate :  değiştirmek demek aslın / fotoğraf yeniden şekilleniyor
                    Guid guidID = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guidID}.jpg"); // root altına foto kaydı için dosya açtık ve yol verdik.
                    article.Image = ($"/images/{guidID}.jpg"); // veritabanına dosya yolunu kaydediyor
                                                               // buraya kadar fotoyu kaydetme kısmı.



                    article.Confirm = AdminConfirm.Confirmed;
                    _articleRepository.Update(article);
                    return RedirectToAction("List");
                }
            }
            else
            {
                dto.Categories = _categoryRepository.GetByDefaults
                  (
                      selector: a => new GetCategoryDTO
                      {
                          ID = a.ID,
                          Name = a.Name
                      },
                       expression: a => a.Statu != Statu.Passive
                  );
            }
            return View(dto);
        }
        public IActionResult Delete(int id)
        {
            Article article = _articleRepository.GetDefault(a => a.ID == id);
            _articleRepository.Delete(article);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Detail(int id)
        {
            // Article sayfasında da profil bilgileri gösterileceği için ArticleDetailVM isimli VM kullanılır
            ArticleDetailVM articleDetailVM = new ArticleDetailVM()
            {
                Article = _articleRepository.GetDefault(a => a.ID == id),

            };

            // Eager loading olduğu için articleDetailVM in Article propertysinin category propertysi doldurulur
            articleDetailVM.Article.Category = _categoryRepository.GetDefault(a => a.ID == articleDetailVM.Article.CategoryID);

            // VM nun Article propertysinin AppUser propertysi doldurulur. Makaleyi yazan kişi
            articleDetailVM.Article.AppUser = _appUserRepository.GetDefault(a => a.ID == articleDetailVM.Article.AppUserID);

            // VM nun UserFollowedCategories listesi doldurulur. Yazarın takip listesi
            articleDetailVM.userFollowedCategories = _categoryRepository.GetCategoryWithUser(articleDetailVM.Article.AppUser.ID);

            // VM nun Yazarın Mail propertysi doldurulur. AppUser nesnesinde Mail kolonu olmadığı için UserManager tablosundan çekilir.
            articleDetailVM.Mail = _userManager.Users.FirstOrDefault(a => a.Id == articleDetailVM.Article.AppUser.IdentityId).Email;

            // VM in Article nesne propertysinin Likes propertysi doldurulur.
            articleDetailVM.Article.Likes = _likeRepository.GetLikes(a => a.ArticleID == articleDetailVM.Article.ID);

            // VM in Article nesne propertysinin comment liste propertysi doldurulur.
            articleDetailVM.Article.Comments = _commentRepository.GetDefaults(a => a.ArticleID == articleDetailVM.Article.ID && a.Statu != Statu.Passive);

            // VM in Article nesne propertsinin comment nesne propertysinin AppUser propertysi yani yorumu yapan kişiler doldurulur.
            foreach (var item in articleDetailVM.Article.Comments)
            {
                item.AppUser = _appUserRepository.GetDefault(a => a.ID == item.AppUserID);
            }

            // Vm in ActiveAppUserID propertsi doldurularak eğer aktif kullanıcının yorumu varsa düzenlenebilir seçeneği eklenir. Yani aktif kullanıcı ActiveAppUser nesnesi doldurulur.
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            articleDetailVM.ActiveAppUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            // Vm in ActiveArticleID propertysi doldurulur. Yorum oluşturulması sırasında kullanılacaktır.
            articleDetailVM.ActiveArticleID = id;

            // Makale başlığına her tıklandığında okuma sayısı 1 artacaktır.
            _articleRepository.Read(articleDetailVM.Article);

            return View(articleDetailVM);
        }

        public IActionResult ListWithFilter(int id)
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
                    expression: a => a.Statu != Statu.Passive && a.CategoryID == id,
                    include: a => a.Include(a => a.AppUser).Include(a => a.Category),
                    orderby: a => a.OrderByDescending(a => a.CreateDate)
                );
            ViewBag.AllCategories = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);
            return View(articles);
        }

        public IActionResult ListWithFilters(List<int> categories)
        {
            if (categories.Count == 0)
            {
                return RedirectToAction("Index", "AppUser");
            }
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

            var newArticleList = articles.Where(article => categories.Contains(article.CategoryID)).ToList();

            
            ViewBag.AllCategories = _categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);

            return View(newArticleList);

        }
        public async Task<IActionResult> Like(int id)
        {
            Article article = _articleRepository.GetDefault(a => a.ID == id);

            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);


            Like like = new Like()
            {
                AppUser = appUser,
                AppUserID = appUser.ID,
                Article = article,
                ArticleID = article.ID
            };

            _likeRepository.Create(like);

            return RedirectToAction("Detail", new
            {
                id = id
            });
        }
        public async Task<IActionResult> Unlike(int id)
        {
            Article article = _articleRepository.GetDefault(a => a.ID == id);

            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            AppUser appUser = _appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            Like like = _likeRepository.GetDefault(a => a.ArticleID == article.ID && a.AppUserID == appUser.ID);

            _likeRepository.Delete(like);
            return RedirectToAction("Detail", new { id = id });
        }
    }
}
