using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class PostCommenEntityConfiguration : IEntityTypeConfiguration<PostCommen>
    {
        public void Configure(EntityTypeBuilder<PostCommen> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("PostCommen");

            #endregion

            #region Properties

            builder.Property(x => x.Text).IsRequired().HasMaxLength(160);
            builder.Property(x => x.IdUser).IsRequired();
            builder.Property(x => x.IdPublication).IsRequired();

            #endregion
        }
    }
}