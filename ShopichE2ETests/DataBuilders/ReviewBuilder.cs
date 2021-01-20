using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichEndToEndTests.DataBuilders
{
	public class ReviewBuilder
	{
		private int _id;
		private int _userId;
		private int _productId;
		private string _text;
		private DateTime _date;
		private int _rating;

		public ReviewBuilder()
		{
			this._id = 0;
			this._userId = 1;
			this._productId = 1;
			this._text = "";
			this._date = new DateTime();
			this._rating = 0;
		}

		public ReviewBuilder WithId(int newId)
		{
			this._id = newId;
			return this;
		}

		public ReviewBuilder WithUserid(int newUserid)
		{
			this._userId = newUserid;
			return this;
		}

		public ReviewBuilder WithText(string newText)
		{
			this._text = newText;
			return this;
		}

		public ReviewBuilder WithDate(string newDate)
		{
			this._date = DateTime.Parse(newDate);
			return this;
		}

		public ReviewBuilder WithRating(int newRating)
		{
			this._rating = newRating;
			return this;
		}

		public ReviewBuilder WithProductId(int newProductid)
		{
			this._productId = newProductid;
			return this;
		}

		public Review Build()
		{
			return new Review
			{
				ReviewId = this._id,
				UserId = this._userId,
				ProductId = this._productId,
				ReviewText = this._text,
				ReviewDate = this._date,
				ReviewRating = this._rating
			};
		}
	}
}
