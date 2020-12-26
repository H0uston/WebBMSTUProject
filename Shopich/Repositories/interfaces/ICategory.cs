using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface ICategory
    {
        public Task<Category[]> GetAll();
        public Task Create(Category entity);
        public Task<Category> GetById(int id);
        public void Update(Category entity);
        public Task Delete(int id);
        public Task Save();
        public IQueryable<Category> Include(params Expression<Func<Category, object>>[] includeProperties);
        public bool Exists(int id);
    }
}