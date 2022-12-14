using mbti_web.Entities;
using mbti_web.Repository.Exceptions;

using Type = mbti_web.Entities.Type;

namespace mbti_web.Repository
{
    public class RepositoryType : IRepositoryType
    {
        private mbti_dbContext db;
        public RepositoryType()
        {
            db = new mbti_dbContext();
        }
        public RepositoryType(mbti_dbContext _db)
        {
            db = _db;
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

            if (t == null)
                throw new TypeNotFoundException(TypeUK);

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

            if (t == null)
                throw new TypeUpdateException(type.Typeuk);

            t.Typedescription = type.Typedescription;

            this.db.SaveChanges();
        }
    }
}
