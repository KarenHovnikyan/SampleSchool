using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll { get; }

        void Create(T entity);

        T GetById(int? id);

        void Update(T entity);

        void Delete(T entity);
    }
}
