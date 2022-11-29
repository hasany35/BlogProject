using BlogProject_5175.DAL.Context;
using BlogProject_5175.DAL.Repositories.Interfaces.Abstract;
using BlogProject_5175.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BlogProject_5175.Models.Enums;

namespace BlogProject_5175.DAL.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ProjectContext context;  // protected yapınca rahat goruyoz
        protected readonly DbSet<T> _table;


        public BaseRepository(ProjectContext context)
        {
            this.context = context;
            _table = context.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _table.Any(expression); 
        }

        public void Create(T entity)
        {
            entity.Statu = Statu.Passive;
            entity.Confirm = AdminConfirm.Waiting;
            _table.Add(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {

            entity.Statu = Statu.Passive;
            entity.RemovedDate = DateTime.Now;
            context.SaveChanges();
        }

        public TResult GetByDefault<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {

            IQueryable<T> query = _table;  // tablomuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık

            if (include!=null)             // include içeren sorgu doluysa
            {
                query = include(query);    // include ait işlemleri yap
            }
            if (expression!=null)         //  expression doluysa
            {
                query = query.Where(expression);  // sorgula
            }
            return  query.Select(selector).First(); // include  ve expression sorgusu dolu/boş gelsede en sonda bu seçim işlemi yapılacaktır.

        }

        public List<TResult> GetByDefaults<TResult>
            (

            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>,
                IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null
            
            )
        {
            // 4 parametreden include(dahil etme) ve filtreleme(expresion) null değilse doldrulur ve devamın ya seçim yapıp alınır yada orderby doluysa orderlanıp seçim  yapıp alınır.

            IQueryable<T> query = _table; // sorgulanabilir yapıya çeviridk tableımızı.

            if (include!=null)            // sorgu doluysa
            {
                query = include(query);
            }
            if (expression!=null)         // expression parametresi doluysa
            {
                query = query.Where(expression);
            }
            if (orderby!=null)            //orderby parametresi doluysa
            {
                return orderby(query).Select(selector).ToList();
            }
            return query.Select(selector).ToList();

        }

        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public void Update(T entity)
        {
            entity.Statu = Statu.Modified;
            entity.ModifiedDate = DateTime.Now;
            _table.Update(entity);
            context.SaveChanges();
        }

        public void Confirmed(T entity)
        {
            entity.Statu = Statu.Modified;
            entity.Confirm=AdminConfirm.Confirmed;
            _table.Update(entity);
            context.SaveChanges();
        }
        public void Rejected(T entity)
        {
            entity.Statu = Statu.Modified;
            entity.Confirm = AdminConfirm.Rejected;
            _table.Update(entity);
            context.SaveChanges();
        }
    }
}
