using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Models
{
    public partial class Type
    {
        public Type()
        {
            //Characters = new HashSet<Character>();
            //Users = new HashSet<User>();
        }

        public int Typeuk { get; set; }
        public string Typename { get; set; }
        public string Typedescription { get; set; }

        //public virtual ICollection<Character> Characters { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
