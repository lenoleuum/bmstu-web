using mbti_web.Repository;

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
        public DateTime Dateofbirth { get; set; }
    }
}
