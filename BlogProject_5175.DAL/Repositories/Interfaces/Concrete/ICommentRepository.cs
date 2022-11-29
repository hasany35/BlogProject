using BlogProject_5175.DAL.Repositories.Interfaces.Abstract;
using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Interfaces.Concrete
{
  public  interface ICommentRepository :IBaseRepository<Comment>
    {
    }
}
