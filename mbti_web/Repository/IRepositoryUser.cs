using mbti_web.Entities;

namespace mbti_web.Repository
{
    public interface IRepositoryUser
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User Find(int UserUK);
        void Remove(User user);
        void Update(User user);
    }
}
