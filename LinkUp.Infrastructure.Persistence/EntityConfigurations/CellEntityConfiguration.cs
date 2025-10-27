using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class CellEntityConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Cells");

            #endregion

            #region Properties

            builder.Property(x => x.X).IsRequired(false);
            builder.Property(x => x.Y).IsRequired(false);
            builder.Property(x => x.CellState).IsRequired().HasDefaultValue((int)CellState.Empty);

            #endregion


        }
    }
}