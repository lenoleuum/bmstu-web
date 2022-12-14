using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Models;

namespace UnitTests.ObjectMother
{
    public class UserModelObjectMother
    {
        public UserModel UnauthorizedUser()
        {
            UserModel user = new UserModel(8753, "", "", "", "", "", "ENTP", DateTime.Now);
            return user;
        }
        public UserModel AuthorizedUser()
        {
            UserModel user = new UserModel();
            user.ID = Guid.NewGuid().GetHashCode();
            user.Login = Guid.NewGuid().ToString();
            user.Password = "password";
            user.Email = "email";
            user.Telagram = "telegram";
            user.Nickname = "user";
            user.Dateofbirth = DateTime.Today;
            user.Typeuk = "ENTP";
            return user;
        }
        public UserModel ExistingUserLena()
        {
            UserModel user = new UserModel();
            user.ID = 1;
            user.Nickname = "lena";
            user.Login = "lena";
            user.Password = "123";
            user.Email = "gusjushka@gmail.com";
            user.Telagram = "@helena.fro";
            user.Typeuk = "INFJ";
            user.Dateofbirth = DateTime.ParseExact("2002-01-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            return user;
        }
        public UserModel ExistingUserMasha()
        {
            UserModel user = new UserModel();
            user.ID = 4;
            user.Nickname = "maryfrost";
            user.Login = "maryfrost";
            user.Password = "1234";
            user.Email = "maryfrost@gmail.com";
            user.Telagram = "@maryfrost";
            user.Typeuk = "INFP";
            user.Dateofbirth = DateTime.ParseExact("1999-11-02", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            return user;
        }
        public UserModel TestUser()
        {
            UserModel user = new UserModel();
            user.ID = Guid.NewGuid().GetHashCode();
            user.Login = Guid.NewGuid().ToString();
            user.Password = "testpassword";
            user.Email = "testemail";
            user.Telagram = "testtelegram";
            user.Nickname = "testuser";
            user.Dateofbirth = DateTime.Today;
            user.Typeuk = "ISTJ";
            return user;
        }
        public UserModel UserForDelete()
        {
            UserModel user = new UserModel();
            user.ID = 100000;
            user.Login = Guid.NewGuid().ToString();
            user.Password = "password";
            user.Email = "email";
            user.Telagram = "telegram";
            user.Nickname = "deleteuser";
            user.Dateofbirth = DateTime.Today;
            user.Typeuk = "ENTP";
            return user;
        }
    }
}
