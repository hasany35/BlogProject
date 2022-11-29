using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogProject_5175.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IAppUserRepository _appUserRepository;

        public CommentController(ICommentRepository commentRepository, IArticleRepository articleRepository, IAppUserRepository appUserRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
            _appUserRepository = appUserRepository;
        }

        public IActionResult List()
        {
            List<Comment> comments = _commentRepository.GetByDefaults
                (
                selector: a => new Comment()
                {
                    AppUser = a.AppUser,
                    Article = a.Article,
                    Text = a.Text,
                    ID = a.ID,
                },
                expression: a => a.Statu != Statu.Passive,
                include: a => a.Include(a => a.AppUser).Include(a => a.Article)
                );

            return View(comments);
        }

        public IActionResult Delete(int id)
        {
            Comment comment = _commentRepository.GetDefault(a => a.ID == id);
            _commentRepository.Delete(comment);

            return RedirectToAction("Detail", "Article", new
            {
                id = comment.ArticleID
            });

        }
        public IActionResult CheckList()
        {
            List<Comment> comments = _commentRepository.GetByDefaults
               (
               selector: a => new Comment()
               {
                   AppUser = a.AppUser,
                   Article = a.Article,
                   Text = a.Text,
                   ID = a.ID,
               },
               expression: a => a.Statu == Statu.Passive && a.Confirm==AdminConfirm.Waiting,
               include: a => a.Include(a => a.AppUser).Include(a => a.Article)
               );

            return View(comments);
        }

        public IActionResult Approve(int id)
        {
            Comment comment = _commentRepository.GetDefault(a => a.ID == id);
            _commentRepository.Confirmed(comment);
            return RedirectToAction("Checklist");
        }

        public IActionResult Reject(int id)
        {
            Comment comment = _commentRepository.GetDefault(a => a.ID == id);
            _commentRepository.Rejected(comment);
            return RedirectToAction("Checklist");
        }
    }
}
