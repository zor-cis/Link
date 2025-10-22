using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Applicacion.Mapping.EntityToDto
{
    public class ReplyEntityMappingProfile : Profile
    {
        public ReplyEntityMappingProfile() 
        { 
            CreateMap<Reply, ReplyDto>()
                .ReverseMap()
                .ForMember(x => x.PostCommen, dto => dto.Ignore());
        }
    }
}
