using Microsoft.EntityFrameworkCore;
using Musicalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Repository.DataContexts
{
    public class MusicLogDBDataContext : DbContext
    {
        public MusicLogDBDataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
