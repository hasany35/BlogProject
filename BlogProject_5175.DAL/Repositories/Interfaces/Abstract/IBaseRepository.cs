using BlogProject_5175.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject_5175.DAL.Repositories.Interfaces.Abstract
{
    public interface IBaseRepository<T> where T:BaseEntity
    {
        void Create(T entity);

        void Update(T entity);
        void Delete(T entity);

        bool Any(Expression<Func<T,bool>> expression);

     

        T GetDefault(Expression<Func<T,bool>> expression);    //tek bir nesne döner

        List<T> GetDefaults(Expression<Func<T,bool>> expression);    // aynı tipte tüm nesneleri döner.

        //aşağıdai metot kullanıldğnda seçim (selector ) için bir linq sorgusu,genel bir seçim için expression parametresi ve ensonda boş da bırakılabilr bir -sorgulanabilir ve dahil edilebilir  bir expression daaha yazılabilir .
        // dönüş tipine karar veremediğimiz/daha komplex tip ddönüşümleri için yukarıdaki doğrudan T dönen metotları değil TRESULT sonucu dönen kendi yazdığımız metotlarımızı kullanabiliriz.
        TResult GetByDefault<TResult>
            (
            Expression<Func<T,TResult>> selector,
            Expression<Func<T,bool>> expression,
            Func<IQueryable<T>,IIncludableQueryable<T,object>> include=null 
            
            );


        List<TResult> GetByDefaults<TResult>
          (
          Expression<Func<T, TResult>> selector,                                     //seçim
          Expression<Func<T, bool>> expression,                                      //sorgu
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,       // içermesini istediğimiz başka tablolar/include ettiğimiz yani dail ettiğimiz sınıflar varsa(opt)
           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null                  // istenen sıralam (opt)

          );

        void Confirmed(T entity);
        void Rejected(T entity);

    }
}
