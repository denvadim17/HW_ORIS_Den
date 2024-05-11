using PokemonApi.DataAccess.Entities;

namespace PokemonApi.Models.PokemonDto
{
    public class PokemonDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Hp { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Speed { get; set; }

        public Breeding Breeding { get; set; }

        public List<PokemonType> PokemonTypes { get; set; }

    }
}
