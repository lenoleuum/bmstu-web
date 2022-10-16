using System;
using System.Collections.Generic;

#nullable disable

namespace mbti_web.Entities
{
    public partial class Character
    {
        public int Characteruk { get; set; }
        public string Charactername { get; set; }
        public int Typeuk { get; set; }
        public string Category { get; set; }

        //public virtual Type TypeukNavigation { get; set; }
    }
}
