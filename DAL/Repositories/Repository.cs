using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected SchoolContext db;
        protected DbSet<T> dbSet;

        public Repository(SchoolContext context)
        {
            db = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll { get { return dbSet.ToList(); } }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public T GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
