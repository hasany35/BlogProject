using BlogProject_5175.Models.Entities.Abstract;
using BlogProject_5175.Models.Enums;

namespace BlogProject_5175.Models.Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        // NAVİGATION PROPERTY
        //1 yorum 1 kişiye aittir
        public int AppUserID { get; set; }
        public AppUser  AppUser { get; set; }

        // 1yorum 1 makaleye aittir.

        public int ArticleID { get; set; }

        public Article  Article { get; set; }


    }
}