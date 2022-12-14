using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;
using Type = mbti_web.Entities.Type;

namespace mbti_web
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<TypeModel, Type>() 
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => src.ID))
                .ForMember(dst => dst.Typename, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Typedescription, opt => opt.MapFrom(src => src.Description))
                ;

            CreateMap<Type, TypeModel>()
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Typeuk))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Typename))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Typedescription))
                ;
        }
    }
}
