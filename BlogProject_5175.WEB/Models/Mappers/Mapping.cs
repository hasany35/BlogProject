using AutoMapper;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.WEB.Areas.Member.Models.DTOs;
using BlogProject_5175.WEB.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_5175.WEB.Models.Mappers
{
    public class Mapping :Profile
    {
        // mapper kütüphanesine yaptırmak istediğimiz mapleme işlemlerini burada tek tek söylememiz gerekiyor.
        //startUpa gitmeden bu sınıfı oluştırduk çünkü orada bize hangi sınıfta mapleme yapıyorsun diye sorulacaktı.

        public Mapping()
        {
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();  // REVERSMAP: iki yönü çalışır.



            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Article, CreateArticleDTO>().ReverseMap();
            CreateMap<Article, UpdateArticleDTO>().ReverseMap();

            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();


        }
    }
}
