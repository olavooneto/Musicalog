using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Musicalog.Domain;
using Musicalog.Domain.Exceptions;
using Musicalog.Domain.Services;
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

        /// <summary>
        /// Retrieve the list of Albums
        /// </summary>
        /// <returns></returns>
        public override async Task<IList<Album>> ListAll() => await this.Context.Albums.Include(x => x.Artists).ToListAsync();

        /// <summary>
        /// Retrieve the Album by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async override ValueTask<Album> GetIdAsync(int id)
        {
            Album? album = await GetAlbumById(id);

            if (album is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            return album;
        }

        /// <summary>
        /// Add a new Album into DB 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async override Task AddAsync(Album entity)
        {
            entity.Artists = GetArtists(entity);

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Update the album into DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task Update(int id, Album entity)
        {
            Album? album = await GetAlbumById(id);

            if (album is null)
                throw new NotFoundException($"{nameof(Album)} was not found.");

            album.Title = entity.Title;
            album.AlbumType = entity.AlbumType;

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
        /// Get the
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        private List<Artist> GetArtists(Album entity)
        {
            List<Artist> artists = entity.Artists.Select(x => Context.Artists.Find(x.Id)).ToList();

            if (artists.Any(x => x is null))
                throw new NotFoundException("Artist Id informed into Music was not found.");
            return artists;
        }

        /// <summary>
        /// Get the Album and Artists associated by Album id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Album> GetAlbumById(int id) => await base.Context.Albums.Include(x => x.Artists).SingleOrDefaultAsync(x => x.Id == id);
    }
}
