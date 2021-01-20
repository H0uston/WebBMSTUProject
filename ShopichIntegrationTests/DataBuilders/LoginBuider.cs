using Shopich.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopichTests.DataBuilders
{
    public class LoginBuilder
    {
        private string _email;
        private string _password;
        private bool _rememberme;

        public LoginBuilder()
        {
            this._email = "";
            this._password = "";
            this._rememberme = false;
        }

        public LoginBuilder WithEmail(string email)
        {
            this._email = email;

            return this;
        }

        public LoginBuilder WithPassword(string password)
        {
            this._password = password;

            return this;
        }

        public LoginBuilder WithRememberMe(bool rememberme)
        {
            this._rememberme = rememberme;

            return this;
        }

        public LoginModel Build()
        {
            return new LoginModel { 
                Email = this._email, 
                Password = this._password, 
                RememberMe = this._rememberme 
            };
        }
    }
}
