﻿using Shopich.Repositories.interfaces;
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
    public class PostgreSQLCategoriesRepository : ICategories
    {
        private ShopichContext _dbContext;

        public PostgreSQLCategoriesRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Categories[]> GetAll()
        {
            return await _dbContext.CategoryCollection.ToArrayAsync();
        }

        public async Task Create(Categories entity)
        {
            await _dbContext.CategoryCollection.AddAsync(entity);
        }

        public async Task<Categories> GetById(int id)
        {
            return await _dbContext.CategoryCollection.FindAsync(id);
        }

        public void Update(Categories entity)
        {
            _dbContext.Update(entity);
        }

        public async void Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.CategoryCollection.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Categories> Include(params Expression<Func<Categories, object>>[] includeProperties)
        {
            IQueryable<Categories> query = _dbContext.CategoryCollection;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.CategoryCollection.Find(id) != null;
        }
    }
}