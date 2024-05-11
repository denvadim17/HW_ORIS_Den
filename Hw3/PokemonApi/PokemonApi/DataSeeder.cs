using Newtonsoft.Json;
using PokemonApi.DataAccess;
using PokemonApi.DataAccess.Entities;

namespace PokemonApi;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        
        if (!_context.Pokemons.Any())
        {
            
            var jsonData = File.ReadAllText("pokemons.json");

            
            var pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(jsonData);
            
            _context.Pokemons.AddRange(pokemons);
            _context.SaveChanges();

            Console.WriteLine("Данные о покемонах успешно добавлены в базу данных.");
        }
        else
        {
            Console.WriteLine("Таблица покемонов уже заполнена данными.");
        }
    }
}