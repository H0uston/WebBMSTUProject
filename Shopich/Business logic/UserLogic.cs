using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class UserLogic
    {
        static public User UpdateUser(User oldUser, User newUser)
        {
            if (newUser.UserPassword != null && newUser.UserPassword != oldUser.UserPassword)
            {
                oldUser.UserPassword = newUser.UserPassword;
            }

            if (newUser.UserName != null && newUser.UserName != oldUser.UserName)
            {
                oldUser.UserName = newUser.UserName;
            }

            if (newUser.UserSurname != null && newUser.UserSurname != oldUser.UserSurname)
            {
                oldUser.UserSurname = newUser.UserSurname;
            }

            if (newUser.UserBirthday != null && newUser.UserBirthday != oldUser.UserBirthday)
            {
                oldUser.UserBirthday = newUser.UserBirthday;
            }

            if (newUser.UserCity != null && newUser.UserCity != oldUser.UserCity)
            {
                oldUser.UserCity = newUser.UserCity;
            }

            if (newUser.UserFlat != null && newUser.UserFlat != oldUser.UserFlat)
            {
                oldUser.UserFlat = newUser.UserFlat;
            }

            if (newUser.UserStreet != null && newUser.UserStreet != oldUser.UserStreet)
            {
                oldUser.UserStreet = newUser.UserStreet;
            }

            if (newUser.UserPhone != null && newUser.UserPhone != oldUser.UserPhone)
            {
                oldUser.UserPhone = newUser.UserPhone;
            }

            return oldUser;
        }
    }
}
