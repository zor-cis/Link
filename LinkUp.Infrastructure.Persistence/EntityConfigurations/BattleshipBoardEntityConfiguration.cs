using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class BattleshipBoardEntityConfiguration : IEntityTypeConfiguration<BattleshipBoard>
    {
        public void Configure(EntityTypeBuilder<BattleshipBoard> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("BattleshipBoards");

            #endregion

            #region Properties

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.GameId).IsRequired();

            #endregion

            #region Relations

            builder.HasMany(x => x.Cells)
                .WithOne(x => x.Board)
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Ships)
                .WithOne(x => x.Board)
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Attacks)
                .WithOne(x => x.TargetBoard)
                .HasForeignKey(x => x.TargetBoardId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}