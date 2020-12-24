using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class TaskBoardRepository : ITaskBoardRepository
    {
        private IProjectRepository _projectRepository;
        private WorkSharpDbContext _context;
        private DbSet<DbTaskBoard> _dbSet;
        public TaskBoardRepository(WorkSharpDbContext context, IProjectRepository projectRepository)
        {
            _context = context;
            _dbSet = _context.TaskBoards;
            _projectRepository = projectRepository;
        }
        public IReadOnlyCollection<DbTaskBoard> GetAll()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<DbTaskBoard> GetUserTaskBoards(Guid userId)
        {
            return _dbSet
                .Include(tb => tb.Team)
                .ThenInclude(t => t.Members)
                .Where(tb => tb.Team.Members.Select(m => m.Id).Contains(userId)).ToList().AsReadOnly();
        }

        public DbTaskBoard GetByIdSecure(Guid taskBoardId, Guid userId)
        {
            var dbTaskBoard = _context.TaskBoards
                .Include(tb => tb.Tasks)
                .ThenInclude(t=>t.Solution)
                .Include(tb=>tb.Project)
                .Include(tb=>tb.Team)
                .ThenInclude(t=>t.Members)
                .FirstOrDefault(t => t.Id.Equals(taskBoardId));
            var projectId = dbTaskBoard.ProjectId;
            if (_projectRepository.IsAdmin(projectId, userId))
            {
                return dbTaskBoard;
            }
            var userTeams = GetUserTaskBoards(userId);
            dbTaskBoard = userTeams.FirstOrDefault(team => team.Id.Equals(taskBoardId));
            return dbTaskBoard;
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
