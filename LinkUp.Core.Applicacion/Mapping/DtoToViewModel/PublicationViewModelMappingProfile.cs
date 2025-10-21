using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.ViewModel.Publication;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class PublicationViewModelMappingProfile : Profile
    {
        public PublicationViewModelMappingProfile() 
        {
            CreateMap<PublicationDto, PublicationViewModel>()
                .ReverseMap()
                .ForMember(x => x.UserId, dto => dto.Ignore());

            CreateMap<PublicationDto, CreatePublicationViewModel>()
                .ReverseMap()
                .ForMember(x => x.ReacctionsCount, dto => dto.Ignore())
                .ForMember(x => x.CommentsCount, dto => dto.Ignore());

           
        }
    }
}
