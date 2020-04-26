﻿using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{

    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(QueryParameters queryParameters);

        Task<T> GetByIdAsync(int id);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(QueryParameters queryParameters, Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindDynamicAsync(QueryParameters queryParameters);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        T Update(T updated);

        Task<T> UpdateAsync(T updated);

        void Delete(T t);

        Task<int> DeleteAsync(T t);

        int Count();

        Task<int> CountAsync();

        IEnumerable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        bool Exist(Expression<Func<T, bool>> predicate);
    }
}
