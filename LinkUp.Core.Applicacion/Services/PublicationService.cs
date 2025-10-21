using AutoMapper;
using LinkUp.Core.Applicacion.Dtos.Publication;
using LinkUp.Core.Applicacion.Dtos.Response;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;

namespace LinkUp.Core.Applicacion.Services
{
    public class PublicationService : GenericService<Publication, PublicationDto>, IPublicationService
    {
        private readonly IAccountServiceForWebApp _user; 
        public PublicationService(IGenericRepository<Publication> repo, IMapper mapper, IAccountServiceForWebApp user) : base(repo, mapper)
        {
            _user = user;
        }

        public override async Task<ResponseDto<PublicationDto>?> AddAsync(PublicationDto dto)
        {
            var response = new ResponseDto<PublicationDto>();
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                response.IsError = true;
                response.MessageResult = "Se debe ingresar un texto";
                return response;
            }

            bool hasImage = !string.IsNullOrWhiteSpace(dto.ImageUrl);
            bool hasVideo = !string.IsNullOrWhiteSpace(dto.VideoUrl);

            if (hasImage && hasVideo)
            {
                response.IsError = true;
                response.MessageResult = "Debe subir una imagen o colocar un enlace de video para realizar la publicacion.";
                return response;
            }

            var userName = await _user.GetUserById(dto.UserId);

            dto.UserName = userName!.UserName;

            return await base.AddAsync(dto);
        }
    }
}
