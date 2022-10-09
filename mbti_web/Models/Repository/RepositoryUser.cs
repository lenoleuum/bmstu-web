namespace mbti_web.Models.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private mbti_dbContext db;
        public RepositoryUser()
        {
            db = new mbti_dbContext();
        }
        public void Add(User user)
        {
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
            return u;
        }
        public void Remove(User user)
        {
            this.db.Users.Remove(user);
            this.db.SaveChanges();
        }
        public void Update(User user)
        {
            var u = this.db.Users.Where(u => u.Useruk == user.Useruk).First();

            u.Telagram = user.Telagram;
            u.Email = user.Email;

            this.db.SaveChanges();
        }
    }
}
