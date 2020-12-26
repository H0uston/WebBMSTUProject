using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface ICategories
    {
        public Task<Categories[]> GetAll();
        public Task Create(Categories entity);
        public Task<Categories> GetById(int id);
        public void Update(Categories entity);
        public void Delete(int id);
        public Task Save();
        public IQueryable<Categories> Include(params Expression<Func<Categories, object>>[] includeProperties);
        public bool Exists(int id);
    }
}