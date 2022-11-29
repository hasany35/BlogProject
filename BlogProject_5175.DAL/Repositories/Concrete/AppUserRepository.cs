using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Abstract;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
   public class AppUserRepository : BaseRepository<AppUser>,IAppUserRepository
    {
        public AppUserRepository(ProjectContext context):base(context)
        {

        }

    }
}
