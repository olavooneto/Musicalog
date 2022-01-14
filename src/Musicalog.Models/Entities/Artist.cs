using Musicalog.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.Entities
{
    public class Artist: BaseEntity
    {
        public string Name { get; set; }

        public IList<Album> Albums { get; set; } = new List<Album>();
    }
}
