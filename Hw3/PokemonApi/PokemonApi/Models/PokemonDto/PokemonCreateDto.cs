namespace PokemonApi.Models.PokemonDto
{
    public class PokemonCreateDto
    {
        public string Name { get; set; }

        public int Hp { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Speed { get; set; }
    }
}
