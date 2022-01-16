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
    public class ArtistServicesTest
    {
        private readonly MusicLogDBDataContext _context;
        private readonly ArtistServices _artistServices;

        public ArtistServicesTest()
        {
            var options = new DbContextOptionsBuilder<MusicLogDBDataContext>().
                UseInMemoryDatabase("fakeDb")
                .Options;
            this._context = new MusicLogDBDataContext(options);
            this._artistServices = new ArtistServices(this._context);

            // Insert Fake data for test
            this._context.Artists.Add(new Artist() { Name = "The Edge" });
            this._context.Artists.Add(new Artist() { Name = "Freddie Mercury" });
            this._context.Artists.Add(new Artist { Name = "Liam Gallagher" });
            this._context.Artists.Add(new Artist { Name = "Noel Gallagher" });

            this._context.SaveChanges();
        }

        [TestMethod]
        public async Task ListAllTest()
        {
            // Action
            var result = await this._artistServices.ListAll();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetIdAsyncTest()
        {
            // Action 
            var expected = this._context.Artists.First();
            var result = await this._artistServices.GetIdAsync(expected.Id);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task AddAsyncTest()
        {
            // Arrange
            var newArtist = new Artist() { Name = "Axl Rose" };

            // Action
            await this._artistServices.AddAsync(newArtist);

            // Assert
            var result = this._context.Artists.Single(x => x.Name == "Axl Rose");
            Assert.AreEqual(newArtist.Name, result.Name);
            Assert.AreEqual(newArtist.Id, result.Id);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            // Arrange
            var artistToModify = this._context.Artists.FirstOrDefault();
            artistToModify.Name = "Slash";
            
            // Action
            await this._artistServices.Update(artistToModify.Id, artistToModify);

            // Assert
            var result = this._context.Artists.Single(x => x.Id == artistToModify.Id);
            Assert.AreEqual(artistToModify.Name, result.Name);
            Assert.AreEqual(artistToModify.Id, result.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var artistToBeDeleted = new Artist()
            {
                Name = "Robert Smith"
            };

            this._context.Add(artistToBeDeleted);
            this._context.SaveChanges();

            // Action
            await this._artistServices.Delete(artistToBeDeleted.Id);


            // Assert
            var result = this._context.Artists.FirstOrDefault(x => x.Id == artistToBeDeleted.Id);
            Assert.IsNull(result, $"{result?.Id} - {result?.Name}");
        }


    }
}
