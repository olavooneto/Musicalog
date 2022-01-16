using Musicalog.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Dtos
{
    public class AlbumForCreationUpdateDto
    {
        public string Title { get; set; }

        public AlbumType AlbumType { get; set; }

        public IList<int> Artists { get; set; } = new List<int>();
    }
}
