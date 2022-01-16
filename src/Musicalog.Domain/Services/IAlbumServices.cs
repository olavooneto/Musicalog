using Musicalog.Domain.Base;
using Musicalog.Models.Entities;

namespace Musicalog.Domain.Services
{
    public interface IAlbumServices : IBaseServices<Album>
    {
        Task Update(int id, Album entity);

        Task Delete(int id);
    }
}
