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

            oldUser.UserName = newUser.UserName;
            oldUser.UserSurname = newUser.UserSurname;
            oldUser.UserBirthday = newUser.UserBirthday;
            oldUser.UserCity = newUser.UserCity;
            oldUser.UserFlat = newUser.UserFlat;
            oldUser.UserStreet = newUser.UserStreet;
            oldUser.UserPhone = newUser.UserPhone;

            return oldUser;
        }
    }
}
