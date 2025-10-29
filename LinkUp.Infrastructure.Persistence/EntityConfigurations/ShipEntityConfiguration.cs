using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class ShipEntityConfiguration : IEntityTypeConfiguration<Ship>
    {
        public void Configure(EntityTypeBuilder<Ship> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Ships");

            #endregion

            #region Properties

            builder.Property(x => x.Size).IsRequired();
            builder.Property(x => x.StartX).IsRequired(false);
            builder.Property(x => x.StartX).IsRequired(false);
            builder.Property(x => x.Direction).IsRequired(false);
            builder.Property(x => x.IsPlaced).IsRequired();
            builder.Property(x => x.IsSunk).IsRequired();

            #endregion

        }
    }
}