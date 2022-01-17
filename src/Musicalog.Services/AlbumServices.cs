using Microsoft.EntityFrameworkCore;
using Musicalog.Domain.Exceptions;
using Musicalog.Domain.Services;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services.Base;

namespace Musicalog.Services
{
    public class AlbumServices : BaseServices<Album>, IAlbumServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumServices"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AlbumServices(MusicLogDBDataContext context) : base(context)
        {

        }

        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns>IList&lt;Album&gt;.</returns>
        public override async Task<IList<Album>> ListAll() => await this.Context.Albums.Include(x => x.Artists).ToListAsync();

        public async Task<IList<Album>> ListAll(string album, string artist)
        {
            IQueryable<Album> query = this.Context.Albums.Include(x => x.Artists);

            // Filter by parameters
            if (!string.IsNullOrEmpty(album))
                query = query.Where(x => x.Title.Contains(album.Trim()));

            if (!string.IsNullOrEmpty(artist))
                query = query.Where(x => x.Artists.Any(a => a.Name.Contains(artist)));

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;Album&gt; representing the asynchronous operation.</returns>
        /// <exception cref="NotFoundException"></exception>
        public async override ValueTask<Album> GetIdAsync(int id)
        {
            Album? album = await GetAlbumById(id);

            if (album is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            return album;
        }

        /// <summary>
        /// Add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async override Task AddAsync(Album entity)
        {
            entity.Artists = GetArtists(entity);

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task Update(int id, Album entity)
        {
            Album? album = await GetAlbumById(id);

            if (album is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            album.Title = entity.Title;
            album.AlbumType = entity.AlbumType;
            album.Stock = entity.Stock;

            // Retrieve the list of artists from Database
            // To update the current list from DB
            var currentArtists = GetArtists(entity);
            album.Artists = currentArtists;


            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var album = await this.GetIdAsync(id);

            if (album is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            this.Context.Remove(album);
            await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>List&lt;Artist&gt;.</returns>
        /// <exception cref="NotFoundException">Artist Id informed into Music was not found.</exception>
        private List<Artist> GetArtists(Album entity)
        {
            List<Artist> artists = entity.Artists.Select(x => Context.Artists.Find(x.Id)).ToList();

            if (artists.Any(x => x is null))
                throw new NotFoundException("Artist Id informed into Music was not found.");
            return artists;
        }

        /// <summary>
        /// Gets the album by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Album.</returns>
        private async Task<Album> GetAlbumById(int id) => await base.Context.Albums.Include(x => x.Artists).SingleOrDefaultAsync(x => x.Id == id);
    }
}
