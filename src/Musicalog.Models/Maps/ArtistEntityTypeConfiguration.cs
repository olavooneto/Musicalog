using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicalog.Models.Entities;
using Musicalog.Models.Maps.Base;

namespace Musicalog.Models.Maps
{
    public class ArtistEntityTypeConfiguration : BaseEntityTypeConfiguration<Artist>
    {
        public override void Configure(EntityTypeBuilder<Artist> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Albums)
                   .WithMany(x => x.Artists);
        }
    }
}
