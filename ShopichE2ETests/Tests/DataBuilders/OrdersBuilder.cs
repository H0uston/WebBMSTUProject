using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichEndToEndTests.DataBuilders
{
    public class OrdersBuilder
    {
        private int _id;
        private int _productid;
        private int _orderid;
        private int _count;

        public OrdersBuilder()
        {
            this._id = 0;
            this._productid = 0;
            this._orderid = 0;
            this._count = 0;
        }

        public OrdersBuilder WithId(int id)
        {
            this._id = id;

            return this;
        }

        public OrdersBuilder WithProductId(int productid)
        {
            this._productid = productid;

            return this;
        }

        public OrdersBuilder WithOrderId(int orderid)
        {
            this._orderid = orderid;

            return this;
        }

        public OrdersBuilder WithCount(int count)
        {
            this._count = count;

            return this;
        }

        public Orders Build()
        {
            return new Orders
            {
                OrdersId = this._id,
                ProductId = this._productid,
                OrderId = this._orderid,
                Count = this._count
            };
        }
    }
}
