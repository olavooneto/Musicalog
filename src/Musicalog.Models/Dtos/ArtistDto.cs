using Musicalog.Models.Dtos.Base;

namespace Musicalog.Models.Dtos
{
    public class ArtistDto : BaseDto
    {
        public string Name { get; set; }

        public IList<AlbumDto> Albums { get; set; } = new List<AlbumDto>();
    }
}
