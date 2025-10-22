using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class PublicationEntityConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Publications");

            #endregion

            #region Properties

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.UserId).HasMaxLength(200);
            builder.Property(x => x.ImageUrl).HasMaxLength(int.MaxValue);
            builder.Property(x => x.VideoUrl).HasMaxLength(int.MaxValue);
            builder.Property(x => x.CreateAt).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(30);
            builder.Property(x => x.PublicationType).IsRequired();

            #endregion
            
            #region Relations

            builder.HasMany(x => x.Comments)
                .WithOne(c => c.Publication)
                .HasForeignKey(x => x.IdPublication)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Reactions)
                .WithOne(c => c.Publication)
                .HasForeignKey(x => x.IdPublication)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}