using AutoMapper;
using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.WEB.Areas.Admin.Models.DTOs;

namespace BlogProject_5175.WEB.Areas.Admin.Models.Mappers
{
    public class Mapping : Profile 
    {
        // Constructor metot içerisinde Mapping atamaları yapılır. Bunun için CreateMap<TSource,TDestination> metodu kullanılır.
        public Mapping()
        {
            // ReverseMap() metodu kullanılarak işlemin her iki tarafa da yapılacağı söylenir. ReverseMap yazdığımız için Source ve Destination nesnelerinin yerleri değiştirilir. 
            // Auto mapping işlemi sırasında hariç tutulması istenen propertyler varsa DoNotValidate metodu ve lambda syntax kullanılarak bu işlem uygulanır

            // Category Controller için gerekli Mapping
   
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

        }
    }
}
