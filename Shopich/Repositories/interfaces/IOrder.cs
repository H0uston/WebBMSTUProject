using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IOrder
    {
        public Task<Order[]> GetAll();
        public Task<Order[]> GetAllAcceptedForUser(int userId);
        public Task Create(Order entity);
        public Task<Order> GetById(int id);
        public Task<Order> GetUnacceptedOrder(int userId);
        public void Update(Order entity);
        public Task Delete(int id);
        public Task Save();
        public IQueryable<Order> Include(params Expression<Func<Order, object>>[] includeProperties);
        public bool Exists(int id);
    }
}