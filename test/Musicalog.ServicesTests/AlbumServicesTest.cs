using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicalog.Domain.Exceptions;
using Musicalog.Models.Entities;
using Musicalog.Repository.DataContexts;
using Musicalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.ServicesTests
{
    [TestClass]
    public class AlbumServicesTest
    {
        private readonly MusicLogDBDataContext _context;
        private readonly AlbumServices _albumServices;

        public AlbumServicesTest()
        {
            var options = new DbContextOptionsBuilder<MusicLogDBDataContext>().
                UseInMemoryDatabase("fakeDb")
                .Options;
            this._context = new MusicLogDBDataContext(options);
            this._albumServices = new AlbumServices(this._context);

            // Insert Fake data for test
            this._context.Artists.Add(new Artist() { Name = "The Edge" });
            this._context.Artists.Add(new Artist() { Name = "Freddie Mercury" });
            this._context.Artists.Add(new Artist { Name = "Liam Gallagher" });
            this._context.Artists.Add(new Artist { Name = "Noel Gallagher" });


            this._context.Albums.Add(new Album()
            {
                Title = "Sunday Blood Sunday",
                AlbumType = Models.Enums.AlbumType.CD,
                Artists = new List<Artist>() {
                 new Artist{Name = "Bono Vox"},
                 new Artist{Name = "The Edge"},
               }
            });

            this._context.SaveChanges();
        }

        [TestMethod]
        public async Task ListAllTest()
        {
            // Action
            var result = await this._albumServices.ListAll();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetIdAsyncTest()
        {
            // Action 
            var expected = this._context.Albums.First();
            var result = await this._albumServices.GetIdAsync(expected.Id);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            // Arrange
            var artistLiam = this._context.Artists.Single(x => x.Name == "Liam Gallagher");
            var artistNoel = this._context.Artists.Single(x => x.Name == "Liam Gallagher");

            var newAlbum = new Album()
            {
                AlbumType = Models.Enums.AlbumType.CD,
                Artists = new List<Artist>()
                {
                    artistLiam, artistNoel
                },
                Title = "Familiar To Milions"
            };

            // Action
            await this._albumServices.AddAsync(newAlbum);

            // Assert
            var result = this._context.Albums.Single(x => x.Title == "Familiar To Milions");
            Assert.AreEqual(newAlbum, result);
            Assert.AreEqual(newAlbum.Artists.Count, result.Artists.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task AddAsyncTest_NotFoundException()
        {
            // Action          
            await this._albumServices.AddAsync(new Album()
            {
                AlbumType = Models.Enums.AlbumType.CD,
                Artists = new List<Artist>()
                {
                    new Artist(){ Id = int.MaxValue}
                },
                Title = "Innuendo"
            });
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            // Arrange
            var artistLiam = this._context.Artists.FirstOrDefault(x => x.Name == "Liam Gallagher");
            var artistNoel = this._context.Artists.FirstOrDefault(x => x.Name == "Noel Gallagher");

            var albumToModify = this._context.Albums.FirstOrDefault();
            albumToModify.Title = "Be Here Now";
            albumToModify.Artists = new List<Artist> { artistLiam, artistNoel };

            // Action
            await this._albumServices.Update(albumToModify.Id, albumToModify);

            // Assert
            var result = this._context.Albums.Single(x => x.Id == albumToModify.Id);
            Assert.AreEqual(albumToModify, result);
            Assert.AreEqual(albumToModify.Artists.Count, result.Artists.Count);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var artistLiam = this._context.Artists.FirstOrDefault(x => x.Name == "Liam Gallagher");
            var artistNoel = this._context.Artists.FirstOrDefault(x => x.Name == "Noel Gallagher");

            var albumToBeDeleted = new Album()
            {
                AlbumType = Models.Enums.AlbumType.CD,
                Artists = new List<Artist> { artistLiam, artistNoel },
                Title = "Standing on the Shoulder of Giants"
            };

            this._context.Add(albumToBeDeleted);
            this._context.SaveChanges();

            // Action
            await this._albumServices.Delete(albumToBeDeleted.Id);


            // Assert
            var result = this._context.Albums.FirstOrDefault(x => x.Id == albumToBeDeleted.Id);
            Assert.IsNull(result, $"{result?.Id} - {result?.Title}");
        }


    }
}
