using LinkUp.Core.Applicacion.ViewModel.Generic;

namespace LinkUp.Core.Applicacion.ViewModel.Publication
{
    public class PublicationViewModel : BasicViewModelForNameId
    {
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public required string UserName { get; set; }
        public required DateTime CreateAt { get; set; }

        public required int PublicationType { get; set; }
        public required int ReacctionsCount { get; set; }
        public required int CommentsCount { get; set; }
    }
}
