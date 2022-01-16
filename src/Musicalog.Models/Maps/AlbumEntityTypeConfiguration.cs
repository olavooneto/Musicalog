using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicalog.Models.Entities;
using Musicalog.Models.Maps.Base;

namespace Musicalog.Models.Maps
{
    public class AlbumEntityTypeConfiguration : BaseEntityTypeConfiguration<Album>
    {
        public override void Configure(EntityTypeBuilder<Album> builder)
        {
            base.Configure(builder);

            builder.Property(x=>x.Title).IsRequired();
            builder.Property(x=>x.AlbumType).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
        }
    }
}
