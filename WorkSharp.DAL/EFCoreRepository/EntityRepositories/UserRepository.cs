using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkSharp.DAL.DbModels;
using WorkSharp.DAL.EFCoreRepository.IEntityRepositories;

namespace WorkSharp.DAL.EFCoreRepository.EntityRepositories
{
    public class UserRepository : IUserRepository
    {
        private WorkSharpDbContext _context;
        private DbSet<DbUser> _dbSet;
        private readonly UserManager<DbUser> _userManager;

        public UserRepository(WorkSharpDbContext context, UserManager<DbUser> userManager)
        {
            _context = context;
            _dbSet = _context.Users;
            _userManager = userManager;
        }

        public IReadOnlyCollection<DbUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public DbUser GetById(Guid userId)
        {
            return _dbSet.AsNoTracking().Include(u => u.Projects).Include(u => u.Teams).Include(u => u.AdminProjects).FirstOrDefault(u => u.Id.Equals(userId));
        }

        public DbUser GetByName(string userName)
        {
            return _dbSet.AsNoTracking().Include(u => u.Projects).Include(u => u.Teams).Include(u => u.AdminProjects).FirstOrDefault(u => u.UserName.Equals(userName));
        }

        public void Detach(DbUser user)
        {
            _context.Entry(_dbSet.Find(user.Id)).State = EntityState.Detached;
        }

        public void UpdateUser(DbUser user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            dbUser.Age = user.Age;
            dbUser.Email = user.Email;
            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            dbUser.Skills = user.Skills;
            dbUser.UserName = user.UserName;

            _context.SaveChanges();
        }
    }
}