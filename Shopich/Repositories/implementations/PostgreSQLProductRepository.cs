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
    public class PostgreSQLProductRepository : IProduct
    {
        private ShopichContext _dbContext;

        public PostgreSQLProductRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Product[]> GetAll()
        {
            return await _dbContext.Products.ToArrayAsync();
        }

        public async Task Create(Product entity)
        {
            await _dbContext.Products.AddAsync(entity);
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            return await _dbContext.Products.Where(p => p.ProductName.ToLower() == name.ToLower()).ToArrayAsync();
        }

        public void Update(Product entity)
        {
            _dbContext.Update(entity);
        }

        public async void Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Products.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> Include(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _dbContext.Products;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.Products.Find(id) != null;
        }
    }
}