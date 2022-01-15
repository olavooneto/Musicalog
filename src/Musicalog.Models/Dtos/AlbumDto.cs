using Musicalog.Models.Dtos.Base;
using Musicalog.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Dtos
{
    public class AlbumDto : BaseDto
    {
        public string Title { get; set; }

        public IList<ArtistDto> Artists { get; set; } = new List<ArtistDto>();

        public AlbumType AlbumType { get; set; }
    }
}
