using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkSharp.DAL.DbModels;

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

        public void Delete(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
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
            //_context.Entry(obj).State = EntityState.Modified;
            _dbSet.Update(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Detach(T obj)
        {
            _context.Entry(obj).State = EntityState.Detached;
        }
    }
}
