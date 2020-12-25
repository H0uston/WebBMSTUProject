using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class OrderLogic
    {
        static public Order ApproveOrder(Order order)
        {
            order.IsApproved = true;

            return order;
        }
    }
}
