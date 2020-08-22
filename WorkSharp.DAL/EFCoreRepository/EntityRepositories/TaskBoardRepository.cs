using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class TaskBoardRepository : ITaskBoardRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbTaskBoard> _dbSet;
        public TaskBoardRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = _context.TaskBoards;
        }
        public IReadOnlyCollection<DbTaskBoard> GetAll()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<DbTaskBoard> GetUserTaskBoards(Guid userId)
        {
            throw new NotImplementedException();
        }

        public DbTaskBoard GetByIdSecure(Guid id, Guid userId)
        {
            return _dbSet.Find(id);
        }

        public bool DeleteSecure(Guid id, Guid userId)
        {
            _dbSet.Remove(_dbSet.Find(id));
            return true; //bad
        }

        public void Create(DbTaskBoard dbTaskBoard)
        {
            _dbSet.Add(dbTaskBoard);
        }

        public bool UpdateSecure(DbTaskBoard dbTaskBoard, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
