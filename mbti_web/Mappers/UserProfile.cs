using AutoMapper;
using mbti_web.Entities;
using mbti_web.Models;

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
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => src.Typeuk))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth))
                .ForMember(dst => dst.Useruk, opt => opt.MapFrom(src => src.ID))
                ;

            CreateMap<User, UserModel>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Telagram, opt => opt.MapFrom(src => src.Telagram))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => src.Typeuk))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth))
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Useruk))
                ;

            CreateMap<User, AuthenticateResponse>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Dateofbirth, opt => opt.MapFrom(src => src.Dateofbirth))
                .ForMember(dst => dst.Typeuk, opt => opt.MapFrom(src => src.Typeuk))
                .ForMember(dst => dst.Telagram, opt => opt.MapFrom(src => src.Telagram))
                .ForMember(dst => dst.ID, opt => opt.MapFrom(src => src.Useruk))
                .ForMember(dst => dst.Token, opt => opt.Ignore())
                ;
        }
    }
}