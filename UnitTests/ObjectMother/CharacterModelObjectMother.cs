using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Models;

namespace UnitTests.ObjectMother
{
    public class CharacterModelObjectMother
    {
        public CharacterModel INFJCharacterExist()
        {
            return new CharacterModel(1, "Armin Arlert", "INFJ", "Anime");
        }
        public CharacterModel ISTJCharacterNew()
        {
            return new CharacterModel(Guid.NewGuid().GetHashCode(), "Aki Hayakawa", "ISTJ", "Anime");
        }
        public CharacterModel ENTPCharacterNew()
        {
            return new CharacterModel(Guid.NewGuid().GetHashCode(), "Himeno", "ENTP", "Anime");
        }
        public CharacterModel ENFJCharacterNew()
        {
            return new CharacterModel(Guid.NewGuid().GetHashCode(), "Oikawa Tooru", "ENFJ", "Anime");
        }
        public CharacterModel ESTPCharacterNew()
        {
            return new CharacterModel(Guid.NewGuid().GetHashCode(), "Maki Zenin", "ESTP", "Anime");
        }
    }
}
