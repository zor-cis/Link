using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class FriendshipRequestEntityConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("FriendshipRequests");

            #endregion

            #region Properties
            
            builder.Property(x => x.IdUserRequester).IsRequired();
            builder.Property(x => x.IdUserAddressee).IsRequired();
            builder.Property(x => x.FriendshipRequestStatus).IsRequired().HasDefaultValue((int)FriendshipRequestStatus.Pending);
            builder.Property(x => x.CreatedAt).IsRequired();

            #endregion
        }
    }
}