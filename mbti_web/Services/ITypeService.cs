using mbti_web.Entities;
using mbti_web.Models;
using Type = mbti_web.Entities.Type;

namespace mbti_web.Services
{
    public interface ITypeService
    {
        List<TypeModel> GetAllTypes();
        TypeModel GetTypeByID(int id);
        TypeModel? GetTypeByName(string name);
        List<TypeModel> GetTypeByNameLike(string name);
        void UpdateDesc(TypeModel typeModel);
    }
}
