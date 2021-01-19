using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichIntegrationTests.DataBuilders
{
    public class CategoriesBuilder
    {
        private int _id;
        private int _productid;
        private int _categoryid;

        public CategoriesBuilder()
        {
            this._id = 0;
            this._productid = 0;
            this._categoryid = 0;
        }

        public CategoriesBuilder WithId(int id)
        {
            this._id = id;

            return this;
        }

        public CategoriesBuilder WithProductId(int productid)
        {
            this._productid = productid;

            return this;
        }

        public CategoriesBuilder WithCategoryId(int categoryid)
        {
            this._categoryid = categoryid;

            return this;
        }

        public Categories Build()
        {
            return new Categories
            {
                CategoriesId = this._id,
                ProductId = this._productid,
                CategoryId = this._categoryid
            };
        }
    }
}
