namespace mbti_web.Models.Repository
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
