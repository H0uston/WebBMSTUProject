using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichTests.DataBuilders
{
    public class CategoryBuilder
    {
        private int _id;
        private string _name;
        private string _description;

        public CategoryBuilder()
        {
            this._id = 0;
            this._name = "";
            this._description = "";
        }

        public CategoryBuilder WithId(int id)
        {
            this._id = id;

            return this;
        }

        public CategoryBuilder WithName(string name)
        {
            this._name = name;

            return this;
        }

        public CategoryBuilder WithDescription(string description)
        {
            this._description = description;

            return this;
        }

        public Category Build()
        {
            return new Category { CategoryId = this._id, CategoryName = this._name, CategoryDescription = this._description };
        }
    }
}
