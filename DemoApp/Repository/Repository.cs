using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DemoApp.Data.Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _set;

        internal Repository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Set.AsEnumerable();
        }

        public IEnumerable<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).AsEnumerable();
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }
        
        public TEntity Add(TEntity entity)
        {
            return Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }
        
        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}
