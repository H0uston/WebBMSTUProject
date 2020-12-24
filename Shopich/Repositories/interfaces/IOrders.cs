using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IOrders
    {
        public Task<Orders[]> GetAll();
        public void Create(Orders entity);
        public Task<Orders> GetById(int id);
        public Task<Orders[]> GetProductsInCart(int orderId);
        public void Update(Orders entity);
        public void Delete(int id);
        public void Save();
        public IQueryable<Orders> Include(params Expression<Func<Orders, object>>[] includeProperties);
        public bool Exists(int id);
    }
}