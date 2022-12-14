using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Entities
{
    public partial class Character
    {
        public Character() { }
        public Character(int _characterUk, string _characterName, int _typeUk, string _category)
        {
            Characteruk = _characterUk;
            Charactername = _characterName;
            Typeuk = _typeUk;
            Category = _category;
        }
        public int Characteruk { get; set; }
        public string Charactername { get; set; }
        public int Typeuk { get; set; }
        public string Category { get; set; }

        //public virtual Type TypeukNavigation { get; set; }
    }
}
