using BlogProject_5175.Models.Entities.Abstract;
using BlogProject_5175.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject_5175.Models.Entities.Concrete
{
    public class Article : BaseEntity
    {
        public Article()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }  // fotograf için -  localdeki dosya yolu.

        [NotMapped] // bu sınıf konfigure edilirken NOTMAPPED denirse bu proprty sql de kolon olarak ayağa kalkmaz.
        public IFormFile ImagePath { get; set; }

        //NAVIGATION PROPERTY

        // 1 makalenin 1 kategorisi olabilir.
        public int CategoryID { get; set; }
        public int ReadCounter { get; set; }
        public int? ReadingTime => Content.Length / 50;

        public Category  Category { get; set; }

        // 1 makalenin 1 oluşturucusu/kullanıcısı

        public int AppUserID { get; set; }
        public AppUser  AppUser { get; set; }

        //1 makalenin çokça beğenisi

        public List<Like>  Likes { get; set; }

        // 1 makalenin çokça yorumu 

        public List<Comment>  Comments { get; set; }


    }
}