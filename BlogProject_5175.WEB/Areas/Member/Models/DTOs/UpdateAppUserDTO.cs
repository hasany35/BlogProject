using Microsoft.AspNetCore.Http;

namespace BlogProject_5175.WEB.Areas.Member.Models.DTOs
{
    public class UpdateAppUserDTO
    {
        public int ID { get; set; } 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public string IdentityId { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Image { get; set; }
        public string Mail { get; set; }
        public IFormFile ImagePath { get; set; }
        public string oldPassword { get; set; }
        public string oldImage { get; set; }

    }
}
