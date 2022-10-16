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
        public IEnumerable<Type> GetAllTypes()
        {
            return _repType.GetAll();
        }
        public Type GetTypeByID(int id)
        {
            return _repType.Find(id);
        }
        public Type? GetTypeByName(string name)
        {
            var type = _repType.GetAll().FirstOrDefault(t => t.Typename == name);

            return type;
        }
        public void UpdateDesc(TypeModel typeModel)
        {
            var type = _mapper.Map<Type>(typeModel);
            _repType.Update(type);
        }
    }
}
