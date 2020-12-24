using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbSolution> _dbSet;
        public SolutionRepository(WorkSharpDbContext context)
        {
            _context = context;
            _dbSet = _context.Solutions;
        }
        public DbSolution GetSolution(Guid solutionId)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(s => s.Id.Equals(solutionId));
        }
    }
}
