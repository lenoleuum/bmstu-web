using mbti_web.Entities;
using mbti_web.Repository.Exceptions;

namespace mbti_web.Repository
{
    public class RepositoryCharacter : IRepositoryCharacter
    {
        private mbti_dbContext db;
        public RepositoryCharacter()
        {
            db = new mbti_dbContext();
        }
        public RepositoryCharacter(mbti_dbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Character> GetAll()
        {
            return db.Characters.ToList();
        }

        // todo: generate new id with guid
        public void Add(Character character)
        {
            if (this.db.Characters.Where(c => c.Characteruk == character.Characteruk).FirstOrDefault() != null)
                throw new CharacterAddException(character.Characteruk);

            db.Characters.Add(character);
            db.SaveChanges();
        }
        public Character Find(int CharacterUK)
        {
            var c = db.Characters.Where(c => c.Characteruk == CharacterUK).FirstOrDefault();

            if (c == null)
                throw new CharacterNotFoundException(CharacterUK);

            return c;
        }
        public void Remove(Character character)
        {
            db.Characters.Remove(character);
            db.SaveChanges();
        }
        public void Update(Character character)
        {
            var c = db.Characters.Where(c => c.Characteruk == character.Characteruk).FirstOrDefault();

            if (c == null)
                throw new CharacterUpdateException(character.Characteruk);

            c.Typeuk = character.Typeuk;

            db.SaveChanges();
        }
    }
}
