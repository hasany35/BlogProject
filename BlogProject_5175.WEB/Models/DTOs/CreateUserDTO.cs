using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Models.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [MinLength(3,ErrorMessage ="en az 3 karakter yazmalısınız")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

      
        public string Image { get; set; }


        [Required]
        public IFormFile ImagePath { get; set; }


        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }


    }
}
