using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class CartLogic
    {
        static public Order CreateUnacceptedOrder(User user)
        {
            var order = new Order();
            order.UserId = user.UserId;
            order.OrderDate = DateTime.UtcNow;
            order.IsApproved = false;

            return order;
        }

        static public Orders AddProductToCart(Order order, Product product, int count)
        {
            var orders = new Orders();
            orders.OrderId = order.OrderId;
            orders.ProductId = product.ProductId;
            orders.Count = count;

            return orders;
        }
    }
}
