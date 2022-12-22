using mbti_web.Repository;
using mbti_web.Entities;
using mbti_web.Models;
using AutoMapper;
using mbti_web.Mappers;

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
        public List<CharacterModel> GetAllCharacters()
        {
            List<CharacterModel> res = new List<CharacterModel>();

            foreach (Character c in _repchar.GetAll())
                res.Add(_mapper.Map<CharacterModel>(c));

            return res;
        }
        public CharacterModel GetCharacterByID(int id)
        {
            return _mapper.Map<CharacterModel>(_repchar.Find(id));
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
        public List<CharacterModel> GetCharacterByType(int type)
        {
            List<CharacterModel> res = new List<CharacterModel>();

            foreach (Character c in _repchar.GetAll().Where(c => c.Typeuk == type).ToList())
                res.Add(_mapper.Map<CharacterModel>(c));

            return res;
        }
        public List<CharacterModel> GetCharacterByType(string type)
        {
            List<CharacterModel> res = new List<CharacterModel>();

            foreach (Character c in _repchar.GetAll().Where(c => c.Typeuk == new TypesDict().getTypeByStr(type)).ToList())
                res.Add(_mapper.Map<CharacterModel>(c));

            return res;
        }
        public List<CharacterModel> GetCharacterByStrLike(string _str, int _flag)
        {
            List<CharacterModel> chars = this.GetAllCharacters().ToList();
            List<CharacterModel> result = new List<CharacterModel>();

            switch (_flag)
            {
                case 1:
                    foreach (CharacterModel c in chars)
                    {
                        if (c.Type.ToLower().Contains(_str.ToLower()))
                        {
                            result.Add(c);
                        }
                    }

                    break;
                case 2:
                    foreach (CharacterModel c in chars)
                    {
                        if (c.Category.ToLower().Contains(_str.ToLower()))
                        {
                            result.Add(c);
                        }
                    }

                    break;
                case 3:
                    foreach (CharacterModel c in chars)
                    {
                        if (c.Name.ToLower().Contains(_str.ToLower()))
                        {
                            result.Add(c);
                        }
                    }

                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
