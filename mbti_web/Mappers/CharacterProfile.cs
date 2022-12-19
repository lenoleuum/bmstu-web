using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;
using mbti_web.Mappers;


namespace mbti_web
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CharacterModel, Character>()
                .ForMember(dst => dst.Characteruk, opt => opt.MapFrom(src => src.ID))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => new TypesDict().getTypeByStr(src.Type)))
                .ForMember(dst => dst.Charactername, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                ;

            CreateMap<Character, CharacterModel>()
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Characteruk))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => new TypesDict().getTypeById(src.Typeuk)))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Charactername))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                ;
        }
    }
}
