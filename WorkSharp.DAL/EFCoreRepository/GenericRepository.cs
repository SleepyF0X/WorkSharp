using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkSharp.DAL.EFCoreRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private WorkSharpDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Create(T obj)
        {
            _dbSet.Add(obj);
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _dbSet.ToList().AsReadOnly();
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
