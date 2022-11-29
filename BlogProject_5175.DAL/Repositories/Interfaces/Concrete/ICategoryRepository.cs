using BlogProject_5175.DAL.Repositories.Interfaces.Abstract;
using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Interfaces.Concrete
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        //List<Category> GetDefaults(Expression<Func<Category, bool>> expression);
        List<Category> GetCategoryWithUser(int id);
    }
}
