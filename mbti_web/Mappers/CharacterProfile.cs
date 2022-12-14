using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;



namespace mbti_web
{
    public class CharacterProfile : Profile
    {
        private Dictionary<int, string> types = new Dictionary<int, string>() // todo: enum
        {
            { 1, "ENTP"},
            { 2, "ENTJ"},
            { 3, "INTJ"},
            { 4, "INTP"},
            { 5, "ENFP"},
            { 6, "ENFJ"},
            { 7, "INFP"},
            { 8, "INFJ"},
            { 9, "ESFJ"},
            { 10, "ESTJ"},
            { 11, "ISFJ"},
            { 12, "ISTJ"},
            { 13, "ESFP"},
            { 14, "ESTP"},
            { 15, "ISFP"},
            { 16, "ISTP"}
        };
        public CharacterProfile()
        {
            CreateMap<CharacterModel, Character>()
                .ForMember(dst => dst.Characteruk, opt => opt.MapFrom(src => src.ID))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => types.FirstOrDefault(x => x.Value == src.Type).Key))
                .ForMember(dst => dst.Charactername, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                ;

            CreateMap<Character, CharacterModel>()
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Characteruk))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => this.types[src.Typeuk]))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Charactername))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                ;
        }
    }
}
