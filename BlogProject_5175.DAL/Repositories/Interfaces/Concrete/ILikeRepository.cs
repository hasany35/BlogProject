using BlogProject_5175.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Interfaces.Concrete
{
    public interface ILikeRepository
    {
        // NOT => like sınıfı baseEnttyden gelmediğinden baseMap/BaseREpodan kalıtım alamaz bu yüzden kendi konfigurasyonunu ve reposunu kendi yazmalıdır.
        void Create(Like entity);

        void Delete(Like entity);

        Like GetDefault(Expression<Func<Like, bool>> expression);
        List<Like> GetLikes(Expression<Func<Like, bool>> expression);
    }
}
