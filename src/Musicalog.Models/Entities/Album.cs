using Musicalog.Models.Entities.Base;
using Musicalog.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Entities
{
    public class Album : BaseEntity
    {
        public string Title { get; set; }

        public IList<Artist> Artists { get; set; } = new List<Artist>();

        public AlbumType AlbumType { get; set; }

        public override bool Equals(object? obj) => Equals(obj as Album);

        public bool Equals(Album other) => other != null
            && other.Title == this.Title
            && other.AlbumType == this.AlbumType;

        public override int GetHashCode() => HashCode.Combine(Title, AlbumType);
    }
}
