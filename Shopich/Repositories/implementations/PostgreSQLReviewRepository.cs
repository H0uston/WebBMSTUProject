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
    public class PostgreSQLReviewRepository : IReview
    {
        private ShopichContext _dbContext;

        public PostgreSQLReviewRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Review[]> GetAll()
        {
            return await _dbContext.Reviews.ToArrayAsync();
        }

        public async Task<Review[]> GetAllByProductId(int productId)
        {
            return await _dbContext.Reviews.Where(r => r.ProductId == productId).ToArrayAsync();
        }

        public async Task Create(Review entity)
        {
            await _dbContext.Reviews.AddAsync(entity);
        }

        public async Task<Review> GetById(int id)
        {
            return await _dbContext.Reviews.FindAsync(id);
        }

        public void Update(Review entity)
        {
            _dbContext.Update(entity);
        }

        public async void Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.Reviews.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Review> Include(params Expression<Func<Review, object>>[] includeProperties)
        {
            IQueryable<Review> query = _dbContext.Reviews;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.Reviews.Find(id) != null;
        }

        public bool ExistsByUserId(int id)
        {
            return _dbContext.Reviews.FirstOrDefault(r => r.UserId == id) != null;
        }
    }
}