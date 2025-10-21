using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class ReactionEntityConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Reactions");

            #endregion

            #region Properties

            builder.Property(x => x.IdPublication).IsRequired();
            builder.Property(x => x.IdUser).IsRequired();
            builder.Property(x => x.ReactionType).IsRequired().HasDefaultValue(0);

            #endregion
        }
    }
}