using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mbti_web.Models;

namespace UnitTests.ObjectMother
{
    public class TypeModelObjectMother
    {
        public TypeModel ENTPType()
        {
            return new TypeModel(1, "ENTP", "A Debater (ENTP) is a person with the Extraverted, Intuitive, Thinking, and Prospecting personality traits. They tend to be bold and creative, deconstructing and rebuilding ideas with great mental agility. They pursue their goals vigorously despite any resistance they might encounter.");
        }
        public TypeModel INTJType()
        {
            return new TypeModel(3, "INTJ", "An Architect (INTJ) is a person with the Introverted, Intuitive, Thinking, and Judging personality traits. These thoughtful tacticians love perfecting the details of life, applying creativity and rationality to everything they do. Their inner world is often a private, complex one.");
        }
        public TypeModel ENTJType()
        {
            return new TypeModel(2, "ENTJ", "A Commander (ENTJ) is someone with the Extraverted, Intuitive, Thinking, and Judging personality traits. They are decisive people who love momentum and accomplishment. They gather information to construct their creative visions but rarely hesitate for long before acting on them.");
        }
        public TypeModel INTPType()
        {
            return new TypeModel(4, "INTP", "A Logician (INTP) is someone with the Introverted, Intuitive, Thinking, and Prospecting personality traits. These flexible thinkers enjoy taking an unconventional approach to many aspects of life. They often seek out unlikely paths, mixing willingness to experiment with personal creativity.");
        }
    }
}
