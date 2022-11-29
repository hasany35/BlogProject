using BlogProject_5175.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Interfaces.Concrete
{
    public interface IUserFollowedCategoryRepository
    {
        void Create(UserFollowedCategory entity);


        void Delete(UserFollowedCategory entity);

        List<UserFollowedCategory> GetDefaults(Expression<Func<UserFollowedCategory, bool>> expression);
        UserFollowedCategory GetDefault(Expression<Func<UserFollowedCategory, bool>> expression);    //tek bir nesne döner


    }
}
