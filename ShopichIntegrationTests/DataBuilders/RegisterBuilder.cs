using Shopich.Models;
using Shopich.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichTests.DataBuilders
{
	public class RegisterBuilder
	{
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

		public RegisterBuilder()
		{
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
		}

		public RegisterBuilder WithEmail(string newEmail)
		{
			this._email = newEmail;

			return this;
		}

		public RegisterBuilder WithPassword(string newPassword)
		{
			this._password = newPassword;

			return this;
		}

		public RegisterBuilder WithPhone(string newPhone)
		{
			this._phone = newPhone;

			return this;
		}

		public RegisterBuilder WithName(string newName)
		{
			this._name = newName;

			return this;
		}

		public RegisterBuilder WithSurname(string newSurname)
		{
			this._surname = newSurname;

			return this;
		}

		public RegisterBuilder WithCity(string newCity)
		{
			this._city = newCity;

			return this;
		}

		public RegisterBuilder WithStreet(string newStreet)
		{
			this._street = newStreet;

			return this;
		}

		public RegisterBuilder WithHouse(string newHouse)
		{
			this._house = newHouse;

			return this;
		}

		public RegisterBuilder WithFlat(string newFlat)
		{
			this._flat = newFlat;

			return this;
		}

		public RegisterBuilder WithIndex(int newIndex)
		{
			this._index = newIndex;

			return this;
		}

		public RegisterBuilder WithBirthdate(string newBirthdate)
		{
			this._birthdate = DateTime.Parse(newBirthdate);

			return this;
		}

		public RegisterModel Build()
		{
			return new RegisterModel
			{
				email = this._email,
				password = this._password,
				phone = this._phone,
				name = this._name,
				surname = this._surname,
				city = this._city,
				street = this._street,
				house = this._house,
				flat = this._flat,
				index = this._index,
				birthday = this._birthdate
			};
		}
	}
}