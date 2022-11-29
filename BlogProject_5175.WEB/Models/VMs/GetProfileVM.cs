using BlogProject_5175.Models.Entities.Concrete;
using System.Collections.Generic;

namespace BlogProject_5175.WEB.Models.VMs
{
    public class GetProfileVM
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }

        public List<Article> Articles { get; set; }

        public List<Category> Categories { get; set; }

    }
}
