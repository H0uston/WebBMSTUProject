using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Repositories.implementations
{
    public class PostgreSQLRoleRepository
    {
        private ShopichContext _dbContext;

        public PostgreSQLRoleRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Role[]> GetAll()
        {
            return await _dbContext.Roles.ToArrayAsync();
        }

        public void Create(Role entity)
        {
            _dbContext.Roles.Add(entity);
        }
        public async Task<Role> GetById(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }
        public void Update(Role entity)
        {
            _dbContext.Update(entity);
        }
        public async void Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Roles.Remove(await this.GetById(id));
            }
        }
        public async void Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        public bool Exists(int id)
        {
            return _dbContext.Users.Find(id) != null;
        }
    }
}
