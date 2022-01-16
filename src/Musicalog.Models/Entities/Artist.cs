using Musicalog.Models.Entities.Base;

namespace Musicalog.Models.Entities
{
    public class Artist: BaseEntity
    {
        public string Name { get; set; }

        public IList<Album> Albums { get; set; } = new List<Album>();
    }
}
