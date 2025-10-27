using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Reply;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class ReplyService : GenericService<Reply, ReplyDto>, IReplyService
    {
        private readonly IReplyRepository _replyRepo;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IMapper _mapper;
        public ReplyService(IGenericRepository<Reply> repo, IMapper mapper, IReplyRepository replyRepo, IAccountServiceForWebApp userService) : base(repo, mapper)
        {
            _replyRepo = replyRepo;
            _mapper = mapper;
            _userService = userService;
        }

       public async Task<ResponseDto<List<ReplyDto>>?> GetAllByCommentAsync(int IdComment)
        {
            var response = new ResponseDto<List<ReplyDto>>();
            try
            {
                var entities = await _replyRepo.GetQuery().Where(p => p.IdPostComment == IdComment).ToListAsync();
                var dtos = _mapper.Map<List<ReplyDto>>(entities);

                foreach(var dto in dtos) 
                {
                    var user = await _userService.GetUserById(dto.IdUser);

                    if(user != null) 
                    { 
                        dto.UserName = user.UserName;
                        dto.ProfileImage = user.ProfileImage;
                    }
                }

                response.IsError = false;
                response.Result = dtos;
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
