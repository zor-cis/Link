using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class AttackEntityConfiguration : IEntityTypeConfiguration<Attack>
    {
        public void Configure(EntityTypeBuilder<Attack> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Attacks");

            #endregion

            #region Properties

            builder.Property(x => x.GameId).IsRequired();
            builder.Property(x => x.AttackerId).IsRequired();
            builder.Property(x => x.TargetBoardId).IsRequired();
            builder.Property(x => x.TargetId).IsRequired();
            builder.Property(x => x.X).IsRequired(false);
            builder.Property(x => x.Y).IsRequired(false);
            builder.Property(x => x.AttackResult).IsRequired().HasDefaultValue((int)AttackResult.Miss);

            #endregion

        }
    }
}