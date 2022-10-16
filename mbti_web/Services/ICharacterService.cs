using mbti_web.Entities;
using mbti_web.Models;

namespace mbti_web.Services
{
    public interface ICharacterService
    {
        IEnumerable<Character> GetAllCharacters();
        Character GetCharacterByID(int id);
        void AddCharacter(CharacterModel charModel);
        void UpdateType(CharacterModel charModel);
    }
}
