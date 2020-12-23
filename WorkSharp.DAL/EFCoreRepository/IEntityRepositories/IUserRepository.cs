using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSharp.DAL.DbModels;

namespace WorkSharp.DAL.EFCoreRepository.IEntityRepositories
{
    public interface IUserRepository
    {
        public IReadOnlyCollection<DbUser> GetAll();
        public DbUser GetById(Guid userId);
        public DbUser GetByName(string userName);
        public void Detach(DbUser user);
        public void UpdateUser(DbUser user);
    }
}
