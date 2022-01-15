using Musicalog.Models.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Dtos
{
    public class ArtistDto : BaseDto
    {
        public string Name { get; set; }

        public IList<AlbumDto> Albums { get; set; } = new List<AlbumDto>();
    }
}
