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
    public class PostgreSQLOrderRepository : IOrder
    {
        private ShopichContext _dbContext;

        public PostgreSQLOrderRepository(ShopichContext context)
        {
            this._dbContext = context;
        }

        public async Task<Order[]> GetAll()
        {
            return await _dbContext.OrderCollection.ToArrayAsync();
        }

        public async Task<Order[]> GetAllAcceptedForUser(int userId)
        {
            return await _dbContext.OrderCollection.Where(o => o.UserId == userId && o.IsApproved == true).ToArrayAsync();
        }

        public async Task Create(Order entity)
        {
            await _dbContext.OrderCollection.AddAsync(entity);
        }

        public async Task<Order> GetById(int id)
        {
            return await _dbContext.OrderCollection.FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> GetUnacceptedOrder(int userId)
        {
            return await _dbContext.OrderCollection.Include(o => o.User).FirstOrDefaultAsync(o => o.User.UserId == userId && o.IsApproved == false);
        }

        public void Update(Order entity)
        {
            _dbContext.Update(entity);
        }

        public async Task Delete(int id)
        {
            if (this.Exists(id))
            {
                _dbContext.OrderCollection.Remove(await this.GetById(id));
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> Include(params Expression<Func<Order, object>>[] includeProperties)
        {
            IQueryable<Order> query = _dbContext.OrderCollection;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public bool Exists(int id)
        {
            return _dbContext.OrderCollection.Find(id) != null;
        }
    }
}