using Microsoft.EntityFrameworkCore;
using PokemonApi.DataAccess.Configurations;
using PokemonApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Breeding> Breedings { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PokemonConfiguration());
            modelBuilder.ApplyConfiguration(new BreedingConfiguration());
        }
    }
}
