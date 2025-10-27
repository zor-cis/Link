using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.FriendshipRequest;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class FriendshipRequestEntityMappingProfile : Profile
    {
        public FriendshipRequestEntityMappingProfile() 
        {
            CreateMap<FriendshipRequest, FriendshipRequestDto>()
                .ReverseMap();        
        }
    }
}
