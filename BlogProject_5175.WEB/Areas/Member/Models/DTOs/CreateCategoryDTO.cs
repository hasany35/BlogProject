using System.ComponentModel.DataAnnotations;

namespace BlogProject_5175.WEB.Areas.Member.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız"), MaxLength(30, ErrorMessage = "en fazla 30 karakter yazmalısınız")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        public string Description { get; set; }


    }
}
