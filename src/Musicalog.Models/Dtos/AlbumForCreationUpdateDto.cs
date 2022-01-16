using Musicalog.Models.Enums;

namespace Musicalog.Models.Dtos
{
    public class AlbumForCreationUpdateDto
    {
        public string Title { get; set; }

        public AlbumType AlbumType { get; set; }

        public IList<int> Artists { get; set; } = new List<int>();

        public bool Stock { get; set; } = true;
    }
}
