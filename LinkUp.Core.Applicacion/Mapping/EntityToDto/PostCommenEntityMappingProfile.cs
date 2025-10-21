using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class PostCommenEntityMappingProfile : Profile
    {
        public PostCommenEntityMappingProfile() 
        { 
            CreateMap<PostCommen, PostCommenDto>()
                .ReverseMap()
                .ForMember(x => x.Publication, dto => dto.Ignore());
        }
    }
}
