using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ProjectContext context;
        private readonly DbSet<Like> _table;

        public LikeRepository(ProjectContext context)
        {
            this.context = context;
            _table = context.Set<Like>();
        }
        public void Create(Like entity)
        {
            _table.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Like entity)
        {
            _table.Remove(entity);
            context.SaveChanges();
        }
        public List<Like> GetLikes(Expression<Func<Like, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
        public Like GetDefault(Expression<Func<Like, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }
    }
}
