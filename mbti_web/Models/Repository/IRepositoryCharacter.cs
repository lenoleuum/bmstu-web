namespace mbti_web.Models.Repository
{
    public interface IRepositoryCharacter
    {
        void Add(Character character);
        IEnumerable<Character> GetAll();
        Character Find(int CharacterUK);
        void Remove(Character character); // not used
        void Update(Character character); // update character type
    }
}
