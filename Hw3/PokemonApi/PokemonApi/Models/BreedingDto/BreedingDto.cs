using PokemonApi.DataAccess.Entities;

namespace PokemonApi.Models.BreedingDto
{
    public class BreedingDto
    {
        public int Id { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public Pokemon Pokemon { get; set; }
    }
}
