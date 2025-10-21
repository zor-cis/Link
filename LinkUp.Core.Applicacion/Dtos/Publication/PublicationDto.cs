using LinkUp.Core.Applicacion.Dtos.Common;

namespace LinkUp.Core.Applicacion.Dtos.Publication
{
    public class PublicationDto : BasicDtoForNameId
    {
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required DateTime CreateAt { get; set; }

        public int PublicationType { get; set; }
        public int ReacctionsCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
