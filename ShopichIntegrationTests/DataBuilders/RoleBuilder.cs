using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichIntegrationTests.DataBuilders
{
	public class RoleBuilder
	{
		private int _id;
		private string _name;
		private string _description;

		public RoleBuilder()
		{
			this._id = 0;
			this._name = "";
			this._description = "";
		}

		public RoleBuilder WithId(int newId)
		{
			this._id = newId;

			return this;
		}

		public RoleBuilder WithName(string newName)
		{
			this._name = newName;

			return this;
		}

		public RoleBuilder WithDescription(string newDescription)
		{
			this._description = newDescription;

			return this;
		}

		public Role Build()
		{
			return new Role { RoleId = this._id, RoleName = this._name, RoleDescription = this._description};
		}
	}
}
