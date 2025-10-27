using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Applicacion.ViewModel.Friend;
using LinkUp.Core.Applicacion.ViewModel.FriendshipRequest;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class FriendshipRequestViewModelMappingProfile : Profile
    {
        public FriendshipRequestViewModelMappingProfile() 
        {    
            CreateMap<FriendshipRequestDto, CreateFriendshipRequestViewModel>()
                .ReverseMap()
                .ForMember(u => u.NameUserFriend, vm => vm.Ignore())
                .ForMember(u => u.ImageProfileFriend, vm => vm.Ignore())
                .ForMember(u => u.CommonFriendsCount, vm => vm.Ignore());

            CreateMap<FriendshipRequestDto, ReceivedFriendshipRequestViewModel>()
                .ReverseMap()
                .ForMember(u => u.CommonFriendsCount, vm => vm.Ignore());

            CreateMap<FriendshipRequestDto, FriendViewModel>()
                .ReverseMap()
                .ForMember(u => u.CommonFriendsCount, vm => vm.Ignore());

            CreateMap<FriendshipRequestDto, SentFriendshipRequestViewModel>()
                .ReverseMap();        
        }
    }
}
