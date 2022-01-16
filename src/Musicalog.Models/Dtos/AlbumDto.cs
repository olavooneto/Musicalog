using Musicalog.Models.Dtos.Base;
using Musicalog.Models.Enums;

namespace Musicalog.Models.Dtos
{
    public class AlbumDto : BaseDto
    {
        public string Title { get; set; }

        public IList<ArtistDto> Artists { get; set; } = new List<ArtistDto>();

        public AlbumType AlbumType { get; set; }

        public bool Stock { get; set; } = true;
    }
}
