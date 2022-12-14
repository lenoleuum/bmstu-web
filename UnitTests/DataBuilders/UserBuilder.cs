using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Entities;

namespace UnitTests.DataBuilders
{
    public class UserBuilder
    {
        private User user;
        public UserBuilder()
        {
            this.user = new User(
                Guid.NewGuid().GetHashCode(),
                Guid.NewGuid().ToString(),
                "password",
                "nickname",
                "email",
                "telegram",
                1,
                DateTime.Today);
        }
        public UserBuilder WithId(int id)
        {
            this.user.Useruk = id;
            return this;
        }
        public UserBuilder WithLogin(string login)
        {
            this.user.Login = login;
            return this;
        }
        public UserBuilder WithPassword(string password)
        {
            this.user.Password = password;
            return this;
        }
        public UserBuilder WithNickname(string nickname)
        {
            this.user.Nickname = nickname;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            this.user.Email = email;
            return this;
        }
        public UserBuilder WithTelegram(string telegram)
        {
            this.user.Telagram = telegram;
            return this;
        }
        public UserBuilder WithType(int typeUk)
        {
            this.user.Typeuk = typeUk;
            return this;
        }
        public UserBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            this.user.Dateofbirth = dateOfBirth;
            return this;
        }
        public User Build()
        {
            return this.user;
        }
    }
}
