﻿namespace BlogProject_5175.WEB.Areas.Admin.Models.VMs
{
    public class GetArticleVM
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string UserFullName { get; set; }
        public string CategoryName { get; set; }
    }
}
