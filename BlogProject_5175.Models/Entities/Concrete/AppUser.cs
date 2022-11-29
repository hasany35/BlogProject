using BlogProject_5175.Models.Entities.Abstract;
using BlogProject_5175.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogProject_5175.Models.Entities.Concrete
{
   public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            UserFollowedCategories = new List<UserFollowedCategory>();
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Password2 { get; set; }

        public string Password3 { get; set; }

        // identity tarafıyla kullanıcıyı eşleştirmek için appuser içinde bir identityId propertysi tanımlayacağız.

        public string IdentityId { get; set; }

      
        public string FullName =>  FirstName+" "+LastName;

        public string Image { get; set; }  // fotograf için -  localdeki dosya yolu.

        [NotMapped] // bu sınıf konfigure edilirken NOTMAPPED denirse bu proprty sql de kolon olarak ayağa kalkmaz.
        public IFormFile ImagePath { get; set; }

        // navigation Property  defaultta EAGER loading yapıyor ki bizde bu projede öyle yapacağız

        // bir userın çokça makalesi olablir.
        public List<Article> Articles { get; set; }

        //1 userın çokça yorumu olabilir
        public List<Comment> Comments { get; set; }

        // 1 usrın çokça beğenisi oalbilir.
        public List<Like> Likes { get; set; }

        //1 user çokça kategoriyi takip etmek isteyebilir.

        public List<UserFollowedCategory>  UserFollowedCategories { get; set; }



    }
}
