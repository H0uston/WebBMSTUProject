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
            return await _dbContext.Categories.ToArrayAsync();
        }

        public void Create(Category entity)
        {
            _dbContext.Categories.Add(entity);
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public void Update(Category entity)
        {
            // _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);
        }

        public async void Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Categories.Remove(await this.GetById(id));
            }
        }

        public async void Save()
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