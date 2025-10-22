using LinkUp.Core.Domain.Common;

namespace LinkUp.Core.Domain.Entities
{
    public class Publication : BasicEntityIdName
    {
        public string? ImageUrl {  get; set; }
        public string? VideoUrl { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required DateTime CreateAt { get; set; }
        public required int PublicationType { get; set; }


        public ICollection<Reaction>? Reactions { get; set; }
        public ICollection<PostCommen>? Comments { get; set; }
    }
}
