using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IRole
    {
        public Task<Role[]> GetAll();
        public Task Create(Role entity);
        public Task<Role> GetById(int id);
        public void Update(Role entity);
        public void Delete(int id);
        public Task Save();
        public bool Exists(int id);
    }
}
