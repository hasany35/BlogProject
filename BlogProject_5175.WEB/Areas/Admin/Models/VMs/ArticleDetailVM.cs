using BlogProject_5175.Models.Entities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject_5175.WEB.Areas.Admin.Models.VMs
{
    public class ArticleDetailVM
    {
        public int? ArticleID { get; set; }
        public Article? Article { get; set; }
        public AppUser? ActiveAppUser { get; set; }
        public string? Mail { get; set; }
        public List<Category>? userFollowedCategories { get; set; }

        [Required(ErrorMessage = "boş yorum yapılamaz.")]
        [MinLength(5, ErrorMessage = "En az 5 karakter yazınız"), MaxLength(200)]
        public string CommentText { get; set; }

        public int? CommentID { get; set; }
        public int ActiveArticleID { get; set; }



    }
}
