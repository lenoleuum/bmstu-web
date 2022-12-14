using mbti_web.Entities;
using Type = mbti_web.Entities.Type;

namespace mbti_web.Repository
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
