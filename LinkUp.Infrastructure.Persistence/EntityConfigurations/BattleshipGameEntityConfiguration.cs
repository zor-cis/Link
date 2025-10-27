using LinkUp.Core.Domain.Common.Enum;
using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class BattleshipGameEntityConfiguration : IEntityTypeConfiguration<BattleshipGame>
    {
        public void Configure(EntityTypeBuilder<BattleshipGame> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("BattleshipGames");

            #endregion

            #region Properties

            builder.Property(x => x.Player1Id).IsRequired();
            builder.Property(x => x.Player2Id).IsRequired();
            builder.Property(x => x.GameStatus).IsRequired().HasDefaultValue((int)GameStatus.SettingUp);
            builder.Property(x => x.TurnStatus).IsRequired().HasDefaultValue((int)TurnStatus.Player1);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.WinnerId).IsRequired(false);
            builder.Property(x => x.EndDate).IsRequired(false);

            #endregion

            #region Relations

            builder.HasMany(x => x.Boards)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}