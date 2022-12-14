using mbti_web.Entities;
using mbti_web.Repository.Exceptions;

namespace mbti_web.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private mbti_dbContext db;
        public RepositoryUser()
        {
            db = new mbti_dbContext();
        }
        public RepositoryUser(mbti_dbContext _db)
        {
            db = _db;
        }

        // todo: generate new id with guid
        public void Add(User user)
        {
            if (!CheckUniqueLogin(user.Login))
                throw new UserAddException(user.Login);

            int NewUK = this.db.Users.Max(a => a.Useruk) + 1;
            user.Useruk = NewUK;

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }
        public IEnumerable<User> GetAll()
        {
            return this.db.Users;
        }
        public User Find(int UserUK)
        {
            var u = this.db.Users.Where(u => u.Useruk == UserUK).FirstOrDefault();

            if (u == null)
                throw new UserNotFoundException(UserUK);

            return u;
        }
        public void Remove(User user)
        {
            var u = this.db.Users.Where(u => u.Useruk == user.Useruk).FirstOrDefault();

            if (u == null)
                throw new UserDeleteException(user.Useruk);

            this.db.Users.Remove(u);
            this.db.SaveChanges();
        }
        public void Update(User user)
        {
            var u = this.db.Users.Where(u => u.Useruk == user.Useruk).FirstOrDefault();

            if (u == null)
                throw new UserUpdateException(user.Useruk);

            u.Telagram = user.Telagram;
            u.Email = user.Email;

            this.db.SaveChanges();
        }
        private bool CheckUniqueId(int id)
        {
            var u = this.db.Users.Where(u => u.Useruk == id).FirstOrDefault();

            if (u == null)
                return true;
            else
                return false;
        }
        private bool CheckUniqueLogin(string login)
        {
            var u = this.db.Users.Where(u => u.Login == login).FirstOrDefault();

            if (u == null)
                return true;
            else
                return false;
        }
    }
}
