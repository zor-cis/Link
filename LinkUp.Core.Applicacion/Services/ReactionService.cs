using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Reaction;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class ReactionService : GenericService<Reaction, ReactionDto>, IReactionService
    {
        private readonly IReactionRepository _reactionRepo;
        private readonly IMapper _mapper;
        public ReactionService(IGenericRepository<Reaction> repo, IMapper mapper, IReactionRepository reactionRepo) : base(repo, mapper)
        {
            _mapper = mapper;
            _reactionRepo = reactionRepo;
        }

        public async Task<ResponseDto<ReactionDto>?> GetByPublicationtAsync(int IdPublication)
        {
            var response = new ResponseDto<ReactionDto>();
            try
            {
                var entity = await _reactionRepo.GetQuery().FirstOrDefaultAsync(r => r.IdPublication == IdPublication);
                response.IsError = false;
                response.Result = _mapper.Map<ReactionDto>(entity);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.MessageResult = ex.Message;
                return response;
            }

            return response;
        }
    }
}
