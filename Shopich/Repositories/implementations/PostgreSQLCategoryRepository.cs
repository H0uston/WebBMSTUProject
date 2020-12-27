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
    public class PostgreSQLCategoryRepository : ICategory
    {
        private ShopichContext _dbContext;

        public PostgreSQLCategoryRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Category[]> GetAll()
        {
            return await _dbContext.Categories.Include(c => c.CategoryCollection).ToArrayAsync();
        }

        public async Task Create(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.Include(c => c.CategoryCollection).FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public void Update(Category entity)
        {
            _dbContext.Update(entity);
        }

        public async Task Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Categories.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Category> Include(params Expression<Func<Category, object>>[] includeProperties)
        {
            IQueryable<Category> query = _dbContext.Categories;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.Categories.Find(id) != null;
        }
    }
}