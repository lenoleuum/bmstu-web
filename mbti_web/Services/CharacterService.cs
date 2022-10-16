using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Models;
using AutoMapper;

namespace mbti_web.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepositoryCharacter _repchar;
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper, IRepositoryCharacter repchar)
        {
            _repchar = repchar;
            _mapper = mapper;
        }
        public IEnumerable<Character> GetAllCharacters()
        {
            return _repchar.GetAll();
        }
        public Character GetCharacterByID(int id)
        {
            return _repchar.Find(id);
        }
        public void AddCharacter(CharacterModel charModel)
        {
            var character = _mapper.Map<Character>(charModel);
            _repchar.Add(character);
        }
        public void UpdateType(CharacterModel charModel)
        {
            var character = _mapper.Map<Character>(charModel);
            _repchar.Update(character);
        }
    }
}
