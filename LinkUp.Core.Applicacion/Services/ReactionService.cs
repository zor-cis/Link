using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;

namespace LinkUp.Core.Applicacion.Services
{
    public class ReactionService : GenericService<Reaction, ReactionDto>, IReactionService
    {
        public ReactionService(IGenericRepository<Reaction> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
