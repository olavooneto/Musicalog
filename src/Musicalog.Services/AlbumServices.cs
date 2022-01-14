using Musicalog.Domain;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services.Base;

namespace Musicalog.Services
{
    public class AlbumServices : BaseServices<Album>, IAlbumServices
    {
        public AlbumServices(MusicLogDBDataContext context) : base(context)
        {
        }
    }
}
