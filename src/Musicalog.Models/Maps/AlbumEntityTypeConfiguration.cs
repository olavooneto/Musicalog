using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicalog.Models.Entities;
using Musicalog.Models.Maps.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Maps
{
    public class AlbumEntityTypeConfiguration : BaseEntityTypeConfiguration<Album>
    {
        public override void Configure(EntityTypeBuilder<Album> builder)
        {
            base.Configure(builder);

            builder.Property(x=>x.Title).IsRequired();
            builder.Property(x=>x.AlbumType).IsRequired();            
        }
    }
}
