using Musicalog.Domain.Base;
using Musicalog.Models.Entities;

namespace Musicalog.Domain.Services
{
    public interface IArtistServices : IBaseServices<Artist>
    {
        Task Update(int id, Artist artist);

        Task Delete(int id);    
    }
}
