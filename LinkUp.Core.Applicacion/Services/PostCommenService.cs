using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;

namespace LinkUp.Core.Applicacion.Services
{
    public class PostCommenService : GenericService<PostCommen, PostCommenDto>, IPostCommenService
    {
        public PostCommenService(IGenericRepository<PostCommen> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
