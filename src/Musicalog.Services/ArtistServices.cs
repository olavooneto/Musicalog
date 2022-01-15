using Musicalog.Domain;
using Musicalog.Domain.Services;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services.Base;

namespace Musicalog.Services
{
    public class ArtistServices : BaseServices<Artist>, IArtistServices
    {
        public ArtistServices(MusicLogDBDataContext context) : base(context)
        {
        }
    }
}
