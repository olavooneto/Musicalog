using Microsoft.EntityFrameworkCore;
using Musicalog.Domain.Exceptions;
using Musicalog.Domain.Services;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services.Base;

namespace Musicalog.Services
{
    public class ArtistServices : BaseServices<Artist>, IArtistServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistServices"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ArtistServices(MusicLogDBDataContext context) : base(context)
        {
        }

        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns>IList&lt;Artist&gt;.</returns>
        public async override Task<IList<Artist>> ListAll() => await this.Context.Artists.Include(x => x.Albums).ToListAsync();

        /// <summary>
        /// Get identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;Artist&gt; representing the asynchronous operation.</returns>
        /// <exception cref="NotFoundException"></exception>
        public async override ValueTask<Artist> GetIdAsync(int id)
        {
            Artist? artist = await this.Context.Artists.Include(x => x.Albums).SingleOrDefaultAsync(x => x.Id == id);

            if (artist is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            return artist;
        }

        /// <summary>
        /// Add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public override async Task AddAsync(Artist entity)
        {
            await Context.AddAsync(entity);

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task Update(int id, Artist entity)
        {
            var artist = await this.Context.Artists.FindAsync(id);

            if (artist is null)
                throw new NotFoundException($"{nameof(Artist)} was not found.");

            artist.Name = entity.Name;

            await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotFoundException"></exception>
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
