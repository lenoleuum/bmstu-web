namespace mbti_web.Models.Repository
{
    public interface IRepositoryType
    {
        void Add(Type type); // not used
        IEnumerable<Type> GetAll();
        Type Find(int TypeUK);
        void Remove(Type type); // not used
        void Update(Type type); // update description
    }
}
