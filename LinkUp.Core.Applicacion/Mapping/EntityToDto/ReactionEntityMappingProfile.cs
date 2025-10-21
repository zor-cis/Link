using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class ReactionEntityMappingProfile : Profile
    {
        public ReactionEntityMappingProfile() 
        { 
            CreateMap<Reaction, ReactionDto>()
                .ReverseMap()
                .ForMember(x => x.Publication, dto => dto.Ignore());
        }
    }
}
