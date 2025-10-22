using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.ViewModel.Reply;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class ReplyViewModelMappingProfile : Profile
    {
        public ReplyViewModelMappingProfile() 
        {
            CreateMap<ReplyDto, ReplyViewModel>()
                .ReverseMap()
                .ForMember(x => x.IdUser, vm => vm.Ignore())
                .ForMember(x => x.IdPostComment, vm => vm.Ignore());

            CreateMap<ReplyDto, CreateReplyViewModel>()
                .ReverseMap();

           
        }
    }
}
