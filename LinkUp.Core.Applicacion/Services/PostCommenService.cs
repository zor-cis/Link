using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.PostCommen;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUp.Core.Applicacion.Services
{
    public class PostCommenService : GenericService<PostCommen, PostCommenDto>, IPostCommenService
    {
        private readonly IPostCommenRepository _postCommenRepo;
        private readonly IAccountServiceForWebApp _userService;
        private readonly IMapper _mapper;
        public PostCommenService(IGenericRepository<PostCommen> repo, IMapper mapper, IPostCommenRepository postCommenRepo, IAccountServiceForWebApp userService) : base(repo, mapper)
        {
            _postCommenRepo = postCommenRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ResponseDto<List<PostCommenDto>>?> GetAllByPublicationAsync(int IdPublication)
        {
            var response = new ResponseDto<List<PostCommenDto>>();
            try
            {
                var entities = await _postCommenRepo.GetQuery().Where(p => p.IdPublication == IdPublication).ToListAsync();
                var dtos = _mapper.Map<List<PostCommenDto>>(entities);

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
