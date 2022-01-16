using Musicalog.Domain.Base;
using Musicalog.Models.Entities;

namespace Musicalog.Domain.Services
{
    public interface IAlbumServices : IBaseServices<Album>
    {
        Task<IList<Album>> ListAll(string album, string artist);

        Task Update(int id, Album entity);

        Task Delete(int id);
    }
}
