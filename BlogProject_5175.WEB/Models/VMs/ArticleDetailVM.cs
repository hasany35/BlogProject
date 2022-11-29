using BlogProject_5175.Models.Entities.Concrete;
using System.Collections.Generic;

namespace BlogProject_5175.WEB.Models.VMs
{
    public class ArticleDetailVM
    {
        public int ArticleID { get; set; }
        public Article Article { get; set; }
        public string Mail { get; set; }
        public List<Category> userFollowedCategories { get; set; }
    

    }
}
