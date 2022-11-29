using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Abstract;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
   public class ArticleRepository :BaseRepository<Article>,IArticleRepository
    {
        public ArticleRepository(ProjectContext context):base(context)
        {

        }
        public void Read(Article article)
        {
            article.ReadCounter++;
            _table.Update(article);
            context.SaveChanges();
        }
    }
}
