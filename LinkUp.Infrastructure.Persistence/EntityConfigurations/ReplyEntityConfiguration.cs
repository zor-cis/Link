using LinkUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkUp.Infrastructure.Persistence.EntityConfigurations
{
    public class ReplyEntityConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            #region Basic Configuration

            builder.HasKey(x => x.Id);
            builder.ToTable("Reply");

            #endregion

            #region Properties

            builder.Property(x => x.ReplyComment).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IdUser).IsRequired();
            builder.Property(x => x.IdPostComment).IsRequired();

            #endregion
        }
    }
}