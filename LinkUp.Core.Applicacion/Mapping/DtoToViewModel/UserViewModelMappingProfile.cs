using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.ViewModel.User;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class UserViewModelMappingProfile : Profile
    {
        public UserViewModelMappingProfile() 
        {
            CreateMap<UserDto, UserViewModel>()
                .ReverseMap();

            CreateMap<UserDto, EditUserViewModel>()
                .ForMember(u => u.Password, dto => dto.Ignore())
                .ForMember(u => u.ConfirmPassword, dto => dto.Ignore())                
                .ForMember(u => u.ProfileImage, dto => dto.Ignore());

            CreateMap<EditUserResponseDto, UserViewModel>()
                .ReverseMap();
            
            CreateMap<LoginResponseDto, UserViewModel>()
                .ReverseMap();
            
            CreateMap<RegisterResponseDto, UserViewModel>()
                .ReverseMap();

            CreateMap<LoginDto, LoginViewModel>()
                .ReverseMap();
            
            CreateMap<SaveUserDto, RegisterUserViewModel>()
                .ForMember(u => u.ConfirmPassword, dto => dto.Ignore())
                .ReverseMap();
            
            CreateMap<SaveUserDto, EditUserViewModel>()
                .ForMember(u => u.ConfirmPassword, dto => dto.Ignore())
                .ReverseMap();   
        }
    }
}
