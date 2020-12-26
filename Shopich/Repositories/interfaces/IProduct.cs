using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IProduct
    {
        public Task<Product[]> GetAll();
        public Task Create(Product entity);
        public Task<Product> GetById(int id);
        public void Update(Product entity);
        public void Delete(int id);
        public Task Save();
        public IQueryable<Product> Include(params Expression<Func<Product, object>>[] includeProperties);
        public bool Exists(int id);
    }
}