using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;
using mbti_web.Mappers;

namespace mbti_web
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>() // тип-источник, тип-приемник
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Telagram, opt => opt.MapFrom(src => src.Telagram))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => new TypesDict().getTypeByStr(src.Typeuk)))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth))
                .ForMember(dst => dst.Useruk, opt => opt.MapFrom(src => src.ID))
                ;

            CreateMap<User, UserModel>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Telagram, opt => opt.MapFrom(src => src.Telagram))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => new TypesDict().getTypeById(src.Typeuk)))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth))
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Useruk))
                ;

            CreateMap<User, AuthenticateResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth.ToString("yyyy-MM-dd")))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => new TypesDict().getTypeById(src.Typeuk)))
                .ForMember(dst => dst.Telagram, opt => opt.MapFrom(src => src.Telagram))
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Useruk))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;

            CreateMap<AuthenticateRequest, User>()
                .ForMember(dst => dst.Email, opt => opt.Ignore())
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Nickname, opt => opt.Ignore())
                .ForMember(dst => dst.Dateofbirth, opt => opt.Ignore())
                .ForMember(dst => dst.Typeuk, opt => opt.Ignore())
                .ForMember(dst => dst.Telagram, opt => opt.Ignore())
                .ForMember(dst => dst.Useruk, opt => opt.Ignore())
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                ;
        }
    }
}