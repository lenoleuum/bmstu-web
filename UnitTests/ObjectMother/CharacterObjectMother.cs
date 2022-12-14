using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Entities;

namespace UnitTests.ObjectMother
{
    public class CharacterObjectMother
    {
        public Character INFJCharacterExist()
        {
            return new Character(1, "Armin Arlert", 8, "Anime");
        }
        public Character ISTJCharacterNew()
        {
            return new Character(Guid.NewGuid().GetHashCode(), "Aki Hayakawa", 12, "Anime");
        }
        public Character ENTPCharacterNew()
        {
            return new Character(Guid.NewGuid().GetHashCode(), "Himeno", 1, "Anime");
        }
        public Character ENFJCharacterNew()
        {
            return new Character(Guid.NewGuid().GetHashCode(), "Oikawa Tooru", 6, "Anime");
        }
        public Character ESTPCharacterNew()
        {
            return new Character(Guid.NewGuid().GetHashCode(), "Maki Zenin", 14, "Anime");
        }
    }
}
