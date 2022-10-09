namespace mbti_web.Models.Repository
{

    public class RepositoryType : IRepositoryType
    {
        private mbti_dbContext db;
        public RepositoryType()
        {
            db = new mbti_dbContext();
        }
        public void Add(Type type)
        {
            this.db.Types.Add(type);
            this.db.SaveChanges();
        }
        public IEnumerable<Type> GetAll()
        {
            return this.db.Types.ToList();
        }
        public Type Find(int TypeUK)
        {
            var t = this.db.Types.Where(t => t.Typeuk == TypeUK).FirstOrDefault();
            return t;
        }
        public void Remove(Type type)
        {
            this.db.Types.Remove(type);
            this.db.SaveChanges();
        }
        public void Update(Type type)
        {
            var t = this.db.Types.Where(t => t.Typeuk == type.Typeuk).First();

            t.Typedescription = type.Typedescription;

            this.db.SaveChanges();
        }
    }
}
