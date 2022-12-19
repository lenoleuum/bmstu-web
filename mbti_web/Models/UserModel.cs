using mbti_web.Repository;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace mbti_web.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Telagram { get; set; }
        public string Typeuk { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public UserModel() { }
        public UserModel(int _id, string _login, string _password, string _nickname, string _email,
                            string _telegram, string _typeuk, DateTime _dateOfBirth)
        {
            this.ID = _id;
            this.Login = _login;
            this.Password = _password;
            this.Nickname = _nickname;
            this.Telagram = _telegram;
            this.Typeuk = _typeuk;
            this.Dateofbirth = _dateOfBirth.Date;
            this.Email = _email;
        }
    }
}
