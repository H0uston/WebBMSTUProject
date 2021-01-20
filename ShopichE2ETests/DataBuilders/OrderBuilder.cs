using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichEndToEndTests.DataBuilders
{
	public class OrderBuilder
	{
		private int _id;
		private int _userId;
		private DateTime _orderDate;
		private bool _isApproved;

		public OrderBuilder()
		{
			this._id = 0;
			this._userId = 1;
			this._orderDate = new DateTime();
			this._isApproved = false;
		}

		public OrderBuilder WithId(int newId)
		{
			this._id = newId;
			return this;
		}

		public OrderBuilder WithUserid(int newUserid)
		{
			this._userId = newUserid;
			return this;
		}

		public OrderBuilder WithDate(string newDate)
		{
			this._orderDate = DateTime.Parse(newDate);
			return this;
		}

		public OrderBuilder WithIsApproved(bool newIsApproved)
		{
			this._isApproved = newIsApproved;
			return this;
		}

		public Order Build()
		{
			return new Order { OrderId = this._id, UserId = this._userId, OrderDate = this._orderDate, IsApproved = this._isApproved };
		}
	}
}
