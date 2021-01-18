using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichTests.DataBuilders
{
	public class UserBuilder
	{
		private int _id;
		private string _email;
		private string _password;
		private string _phone;
		private string _name;
		private string _surname;
		private string _city;
		private string _street;
		private string _house;
		private string _flat;
		private int _index;
		private DateTime _birthdate;
		private int _roleId;

		public UserBuilder()
		{
			this._id = 0;
			this._email = "";
			this._password = "";
			this._phone = "";
			this._name = "";
			this._surname = "";
			this._city = "";
			this._street = "";
			this._house = "";
			this._flat = "";
			this._index = 0;
			this._birthdate = new DateTime();
			this._roleId = 0;
		}

		public UserBuilder WithId(int newId)
		{
			this._id = newId;

			return this;
		}

		public UserBuilder WithEmail(string newEmail)
		{
			this._email = newEmail;

			return this;
		}

		public UserBuilder WithPassword(string newPassword)
		{
			this._password = newPassword;

			return this;
		}

		public UserBuilder WithPhone(string newPhone)
		{
			this._phone = newPhone;

			return this;
		}

		public UserBuilder WithName(string newName)
		{
			this._name = newName;

			return this;
		}

		public UserBuilder WithSurname(string newSurname)
		{
			this._surname = newSurname;

			return this;
		}

		public UserBuilder WithCity(string newCity)
		{
			this._city = newCity;

			return this;
		}

		public UserBuilder WithStreet(string newStreet)
		{
			this._street = newStreet;

			return this;
		}

		public UserBuilder WithHouse(string newHouse)
		{
			this._house = newHouse;

			return this;
		}

		public UserBuilder WithFlat(string newFlat)
		{
			this._flat = newFlat;

			return this;
		}

		public UserBuilder WithIndex(int newIndex)
		{
			this._index = newIndex;

			return this;
		}

		public UserBuilder WithBirthdate(string newBirthdate)
		{
			this._birthdate = DateTime.Parse(newBirthdate);

			return this;
		}

		public UserBuilder WithRoleId(int newRoleid)
		{
			this._roleId = newRoleid;

			return this;
		}

		public UserBuilder WithNoRoleId()
		{
			this._roleId = 3;

			return this;
		}

		public User Build()
		{
			return new User
			{
				UserId = this._id,
				UserEmail = this._email,
				UserPassword = this._password,
				UserPhone = this._phone,
				UserName = this._name,
				UserSurname = this._surname,
				UserCity = this._city,
				UserStreet = this._street,
				UserHouse = this._house,
				UserFlat = this._flat,
				UserIndex = this._index,
				RoleId = this._roleId,
				UserBirthday = this._birthdate
			};
		}
	}
}
