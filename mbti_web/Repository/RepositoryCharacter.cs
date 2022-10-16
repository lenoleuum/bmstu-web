using mbti_web.Entities;

namespace mbti_web.Repository
{
    public class RepositoryCharacter : IRepositoryCharacter
    {
        private mbti_dbContext db;
        public RepositoryCharacter()
        {
            db = new mbti_dbContext();
        }
        public IEnumerable<Character> GetAll()
        {
            return db.Characters.ToList();
        }
        public void Add(Character character)
        {
            db.Characters.Add(character);
            db.SaveChanges();
        }
        public Character Find(int CharacterUK)
        {
            var c = db.Characters.Where(c => c.Characteruk == CharacterUK).FirstOrDefault();
            return c;
        }
        public void Remove(Character character)
        {
            db.Characters.Remove(character);
            db.SaveChanges();
        }
        public void Update(Character character)
        {
            var c = db.Characters.Where(c => c.Characteruk == character.Characteruk).First();
            
            c.Typeuk = character.Typeuk;

            db.SaveChanges();
        }
    }
}
