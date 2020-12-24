using System;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface ISolutionRepository
    {
        public DbSolution GetSolution(Guid solutionId);
    }
}