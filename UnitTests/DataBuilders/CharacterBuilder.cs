using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Entities;

namespace UnitTests.DataBuilders
{
    public class CharacterBuilder
    {
        private Character character;
        public CharacterBuilder() // int32 - GetHashCode(); в бд тоже guid/uuid
        {
            this.character = new Character(Guid.NewGuid().GetHashCode(), "NewCharacter", 1, "NewCategory");
        }
        public CharacterBuilder WithId(int id)
        {
            character.Characteruk = id;
            return this;
        }
        public CharacterBuilder WithName(string name)
        {
            character.Charactername = name;
            return this;
        }
        public CharacterBuilder WithType(int typeId)
        {
            character.Typeuk = typeId;
            return this;
        }
        public CharacterBuilder WithCategory(string category)
        {
            character.Category = category;
            return this;
        }
        public Character Build()
        {
            return this.character;
        }
    }
}
