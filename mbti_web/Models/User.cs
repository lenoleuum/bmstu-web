using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Models
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
    }
}
