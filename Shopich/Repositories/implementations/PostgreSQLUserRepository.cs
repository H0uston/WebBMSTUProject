using Shopich.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections;
using Shopich.Models;

namespace Shopich.Repositories.implementations
{
    public class PostgreSQLUserRepository : IUser
    {
        private ShopichContext _dbContext;

        public PostgreSQLUserRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<User[]> GetAll()
        {
            return await _dbContext.Users.ToArrayAsync();
        }

        public async Task Create(User item)
        {
            await _dbContext.Users.AddAsync(item);
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }

        public void Update(User entity)
        {
            _dbContext.Update(entity);
        }

        public async Task Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Users.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<User> Include(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = _dbContext.Users;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.Users.Find(id) != null;
        }
    }
}