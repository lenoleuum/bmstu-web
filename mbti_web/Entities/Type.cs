using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Entities
{
    public partial class Type
    {
        public Type()
        {
            //Characters = new HashSet<Character>();
            //Users = new HashSet<User>();
        }

        public Type(int _typeUk, string _typeName, string _typeDescription)
        {
            Typeuk = _typeUk;
            Typename = _typeName;
            Typedescription = _typeDescription;
        }

        public int Typeuk { get; set; }
        public string Typename { get; set; }
        public string Typedescription { get; set; }

        //public virtual ICollection<Character> Characters { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
