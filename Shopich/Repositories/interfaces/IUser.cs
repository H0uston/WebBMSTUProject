using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IUser
    {
        public Task<User[]> GetAll();
        public Task Create(User entity);
        public Task<User> GetById(int id);
        public Task<User> GetByEmail(string email);
        public void Update(User entity);
        public void Delete(int id);
        public Task Save();
        public IQueryable<User> Include(params Expression<Func<User, object>>[] includeProperties);
        public bool Exists(int id);
    }
}
