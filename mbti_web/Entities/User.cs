using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Entities
{
    public partial class User
    {
        public int Useruk { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Telagram { get; set; }
        public int Typeuk { get; set; }
        public DateTime Dateofbirth { get; set; }

        //public virtual Type TypeukNavigation { get; set; }

        public User() { }

        public User(int _userUk, string _login, string _password, string _nickname,
                            string _email, string _telegram, int _typeUk, DateTime _dateOfBirth)
        {
            Useruk = _userUk;
            Login = _login;
            Password = _password;
            Nickname = _nickname;
            Email = _email;
            Telagram = _telegram;
            Typeuk = _typeUk;
            Dateofbirth = _dateOfBirth;
        }
    }
}
