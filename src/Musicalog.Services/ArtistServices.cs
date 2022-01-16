using Microsoft.EntityFrameworkCore;
using Musicalog.Domain;
using Musicalog.Domain.Exceptions;
using Musicalog.Domain.Services;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services.Base;
using System.Linq.Expressions;

namespace Musicalog.Services
{
    public class ArtistServices : BaseServices<Artist>, IArtistServices
    {
        public ArtistServices(MusicLogDBDataContext context) : base(context)
        {
        }

        public async override Task<IList<Artist>> ListAll() => await this.Context.Artists.Include(x => x.Albums).ToListAsync();

        public async override ValueTask<Artist> GetIdAsync(int id)
        {
            Artist? artist = await this.Context.Artists.Include(x => x.Albums).SingleOrDefaultAsync(x => x.Id == id);

            if (artist is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            return artist;
        }

        public override async Task AddAsync(Artist entity)
        {
            await Context.AddAsync(entity);

            await Context.SaveChangesAsync();
        }

        public async Task Update(int id, Artist entity)
        {
            var artist = await this.Context.Artists.FindAsync(id);

            if (artist is null)
                throw new NotFoundException($"{nameof(Artist)} was not found.");

            artist.Name = entity.Name;

            await this.Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var artist = await this.Context.Artists.FindAsync(id);

            if (artist is null)
                throw new NotFoundException($"{nameof(Artist)} was not found.");

            this.Context.Artists.Remove(artist);

            await this.Context.SaveChangesAsync();
        }

    }
}
