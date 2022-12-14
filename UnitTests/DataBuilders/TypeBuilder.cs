using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Entities;

using Type = mbti_web.Entities.Type;

namespace UnitTests.DataBuilders
{
    public class TypeBuilder
    {
        private Type type;
        public TypeBuilder()
        {
            this.type = new Type(Guid.NewGuid().GetHashCode(), "NewType", "Decsription should be here");
        }
        public TypeBuilder WithId(int id)
        {
            type.Typeuk = id;
            return this;
        }
        public TypeBuilder WithName(string name)
        {
            type.Typename = name;
            return this;
        }
        public TypeBuilder WithDescription(string description)
        {
            type.Typedescription = description;
            return this;
        }
        public Type Build()
        {
            return this.type;
        }
    }
}
