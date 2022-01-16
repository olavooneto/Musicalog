using Musicalog.Domain.Base;
using Musicalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Domain.Services
{
    public interface IAlbumServices : IBaseServices<Album>
    {
        Task Update(int id, Album entity);

        Task Delete(int id);
    }
}
