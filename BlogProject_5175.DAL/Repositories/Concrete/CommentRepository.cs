using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Abstract;
using BlogProject_5175.DAL.Repositories.Interfaces.Concrete;
using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Concrete
{
  public  class CommentRepository :BaseRepository<Comment>,ICommentRepository
    {
        public CommentRepository(ProjectContext context):base(context)
        {

        }
    }
}
