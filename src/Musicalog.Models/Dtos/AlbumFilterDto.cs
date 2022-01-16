using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Dtos
{
    public class AlbumFilterDto
    {
        public string Album { get; set; } = string.Empty;

        public string Artist { get; set; } = string.Empty;
    }
}
