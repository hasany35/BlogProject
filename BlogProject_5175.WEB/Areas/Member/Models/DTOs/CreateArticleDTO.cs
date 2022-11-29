using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.Enums;
using BlogProject_5175.WEB.Areas.Member.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Areas.Member.Models.DTOs
{
    public class CreateArticleDTO
    {

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        public string Content { get; set; }

        public string Image { get; set; } 

        [Required]
        [NotMapped]
        public IFormFile ImagePath { get; set; }

        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int AppUserID { get; set; }


        public List<GetCategoryDTO> Categories { get; set; }
     
    }
}
