using mbti_web.Entities;

namespace mbti_web.Models
{
    public class AuthenticateResponse
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Nickname { get; set; } // todo: delete
        public string Email { get; set; }
        public string Telagram { get; set; } // todo: delete
        public int Typeuk { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Token { get; set; }
        public AuthenticateResponse(User u, string token)
        {
            ID = u.Useruk;
            Login = u.Login;
            Nickname = u.Nickname;
            Email = u.Email;
            Telagram = u.Telagram;
            Typeuk = u.Typeuk;
            Dateofbirth = u.Dateofbirth;
            Token = token;
        }
    }
}
