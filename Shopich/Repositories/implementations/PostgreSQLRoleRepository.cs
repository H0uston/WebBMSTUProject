using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Repositories.implementations
{
    public class PostgreSQLRoleRepository : IRole
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

        public async Task Create(Role entity)
        {
            await _dbContext.Roles.AddAsync(entity);
        }
        public async Task<Role> GetById(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }
        public void Update(Role entity)
        {
            _dbContext.Update(entity);
        }
        public async Task Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Roles.Remove(await this.GetById(id));
            }
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        public bool Exists(int id)
        {
            return _dbContext.Users.Find(id) != null;
        }
    }
}
