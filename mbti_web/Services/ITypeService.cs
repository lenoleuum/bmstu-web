using mbti_web.Entities;
using mbti_web.Models;
using Type = mbti_web.Entities.Type;

namespace mbti_web.Services
{
    public interface ITypeService
    {
        IEnumerable<Type> GetAllTypes();
        Type GetTypeByID(int id);
        Type? GetTypeByName(string name);
        void UpdateDesc(TypeModel typeModel);
    }
}
