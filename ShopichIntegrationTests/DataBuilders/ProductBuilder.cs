using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichIntegrationTests.DataBuilders
{
	public class ProductBuilder
	{
		private int _id;
		private string _name;
		private float _price;
		private bool _availability;
		private int _discount;

		public ProductBuilder()
		{
			this._id = 0;
			this._name = "";
			this._price = 0;
			this._availability = false;
			this._discount = 0;
		}

		public ProductBuilder WithId(int newId)
		{
			this._id = newId;
			return this;
		}

		public ProductBuilder WithName(string newName)
		{
			this._name = newName;
			return this;
		}

		public ProductBuilder WithPrice(float newPrice)
		{
			this._price = newPrice;
			return this;
		}

		public ProductBuilder WithAvailability(bool newAvailability)
		{
			this._availability = newAvailability;
			return this;
		}

		public ProductBuilder WithDiscount(int newDiscount)
		{
			this._discount = newDiscount;
			return this;
		}

		public Product Build()
		{
			return new Product
			{
				ProductId = this._id,
				ProductName = this._name,
				ProductPrice = this._price,
				ProductAvailability = this._availability,
				ProductDiscount = this._discount
			};
		}
	}
}
