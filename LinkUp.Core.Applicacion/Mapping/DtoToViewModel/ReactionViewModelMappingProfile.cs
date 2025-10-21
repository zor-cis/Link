using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.ViewModel.Reaction;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class ReactionViewModelMappingProfile : Profile
    {
        public ReactionViewModelMappingProfile() 
        {
            CreateMap<ReactionDto, ReactionViewModel>()
                .ReverseMap()
                .ForMember(x => x.IdUser, vm => vm.Ignore())
                .ForMember(x => x.IdPublication, vm => vm.Ignore());

            CreateMap<ReactionDto, CreateReactionViewModel>()
                .ReverseMap();

           
        }
    }
}
