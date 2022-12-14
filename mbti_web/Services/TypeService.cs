using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;
using mbti_web.Repository;
using mbti_web.Middleware;
using Type = mbti_web.Entities.Type;

namespace mbti_web.Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepositoryType _repType;
        private readonly IMapper _mapper;
        public TypeService(IMapper mapper, IRepositoryType repType)
        {
            _mapper = mapper;
            _repType = repType;
        }
        public List<TypeModel> GetAllTypes()
        {
            List<TypeModel> res = new List<TypeModel>();

            foreach (Type t in _repType.GetAll())
                res.Add(_mapper.Map<TypeModel>(t));

            return res;
        }
        public TypeModel GetTypeByID(int id)
        {
            return _mapper.Map<TypeModel>(_repType.Find(id));
        }
        public TypeModel? GetTypeByName(string name)
        {
            var type = _repType.GetAll().FirstOrDefault(t => t.Typename == name);

            return _mapper.Map<TypeModel>(type);
        }

        public List<TypeModel> GetTypeByNameLike(string name)
        {
            List<Type> types = _repType.GetAll().Where(t => t.Typename.Contains(name)).ToList();
            List<TypeModel> res = new List<TypeModel>();

            foreach (Type t in types)
                res.Add(_mapper.Map<TypeModel>(t));

            return res;
        }

        public void UpdateDesc(TypeModel typeModel)
        {
            var type = _mapper.Map<Type>(typeModel);
            _repType.Update(type);
        }
    }
}
