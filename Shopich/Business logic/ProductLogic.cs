using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class ProductLogic
    {
        static public IEnumerable<Product> SetRating(IEnumerable<Product> products)
        {
            for (var i = 0; i < products.Count(); i++)
            {
                Product p = products.Skip(i).Take(1).ToArray()[0];
                p.GetProductRating();
            }

            return products;
        }
    }
}
