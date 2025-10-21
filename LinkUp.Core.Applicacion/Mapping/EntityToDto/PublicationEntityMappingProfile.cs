using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class PublicationEntityMappingProfile : Profile
    {
        public PublicationEntityMappingProfile() 
        { 
            CreateMap<Publication, PublicationDto>()
                .ForMember(u => u.CommentsCount, x => x.MapFrom(src => src.Comments != null ? src.Comments.Count : 0))
                .ForMember(u => u.ReacctionsCount, x => x.MapFrom(src => src.Reactions != null ? src.Reactions.Count : 0))
                .ReverseMap()
                .ForMember(u => u.Reactions, dto => dto.Ignore())
                .ForMember(u => u.Comments, dto => dto.Ignore());
        }
    }
}
