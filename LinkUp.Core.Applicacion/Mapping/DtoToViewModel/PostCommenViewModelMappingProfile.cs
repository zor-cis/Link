using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;

namespace LinkUp.Core.Applicacion.Mapping.DtoToViewModel
{
    public class PostCommenViewModelMappingProfile : Profile
    {
        public PostCommenViewModelMappingProfile() 
        {
            CreateMap<PostCommenDto, PostCommenViewModel>()
                .ReverseMap()
                .ForMember(x => x.IdUser, vm => vm.Ignore())
                .ForMember(x => x.IdPublication, vm => vm.Ignore());

            CreateMap<PostCommenDto, CreatePostCommenViewModel>()
                .ReverseMap();

           
        }
    }
}
