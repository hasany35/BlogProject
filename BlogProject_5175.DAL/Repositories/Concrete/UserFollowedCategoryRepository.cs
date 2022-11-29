using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
    public class UserFollowedCategoryRepository : IUserFollowedCategoryRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<UserFollowedCategory> _table;

        public UserFollowedCategoryRepository(ProjectContext context)
        {
            _context = context;
            _table = context.Set<UserFollowedCategory>();
        }
        public void Create(UserFollowedCategory entity)
        {
            _table.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(UserFollowedCategory entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

        public UserFollowedCategory GetDefault(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.FirstOrDefault(expression);
        }

        public List<UserFollowedCategory> GetDefaults(Expression<Func<UserFollowedCategory, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
    }

}
