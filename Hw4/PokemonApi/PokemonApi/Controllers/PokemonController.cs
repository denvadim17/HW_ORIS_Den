using Microsoft.AspNetCore.Mvc;
using PokemonApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using PokemonApi.DataAccess.Entities;
using PokemonApi.Models.PokemonDto;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PokemonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для получения всех покемонов
        /// </summary>
        /// <returns>Возвращает список всех покемонов в системе</returns>
        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            return await _context.Pokemons.ToListAsync();
        }

        /// <summary>
        /// Метод для получения покемона по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор покемона</param>
        /// <returns>Возвращает покемона с указанным идентификатором</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(int id)
        {
            return await _context.Pokemons.FindAsync(id) ?? throw new Exception();
        }
        /// <summary>
        /// Метод для покемонов по указаной строке поиска
        /// </summary>
        /// <returns>Возвращает список всех найденных покенов </returns>
        // TODO: ножно с массивом данных отдавать общее количество фильтрованных данных  (total)
        
        
        [HttpGet("GetByFilter")]
        public async Task<List<Pokemon>> GetByFilter([FromQuery] string? name)
        {
            var result = await _context.Pokemons.ToListAsync();
            return result.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Метод для создания нового покемона
        /// </summary>
        /// <param name="pokemon">Данные нового покемона</param>
        /// <returns>Возвращает созданного покемона</returns>
        [HttpPost]
        public async Task<ActionResult<PokemonCreateDto>> PostPokemon(PokemonCreateDto pokemon)
        {
            var newPoke = new Pokemon()
            {
                Name = pokemon.Name,
                Hp = pokemon.Hp,
                Attack = pokemon.Attack,
                Defense = pokemon.Defense,
                Speed = pokemon.Speed
            };

            _context.Pokemons.Add(newPoke);
            await _context.SaveChangesAsync();

            return pokemon;
        }

        /// <summary>
        /// Метод для обновления существующего покемона
        /// </summary>
        /// <param name="id">Идентификатор существующего покемона</param>
        /// <param name="pokemon">Новые данные покемона</param>
        /// <returns>Результат выполнения операции</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon(int id, PokemonUpdateDto pokemon)
        {

            var poke = await _context.Pokemons.FindAsync(id);
            if (poke == null)
            {
                return NotFound();
            }

            poke.Name = pokemon.Name;
            poke.Hp = pokemon.Hp;
            poke.Attack = pokemon.Attack;
            poke.Defense = pokemon.Defense;
            poke.Speed = pokemon.Speed;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Метод для получения всей информации по одному покемону
        /// </summary>
        /// <returns>Возвращает полную информацию о покемоне по заданному Id или Name </returns>
        [HttpGet("NameOrId")]
        public Pokemon GetByIdOrName([FromQuery] string nameOrId)
        {
            int id = 0;
            if (int.TryParse(nameOrId, out id))
            {
                return _context.Pokemons.FirstOrDefault(p => p.Id == id);
            }
            else
            {
                return _context.Pokemons.FirstOrDefault(p => p.Name.ToLower() == nameOrId.ToLower());
            }
        }

        /// <summary>
        /// Метод для удаления покемона по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор покемона для удаления</param>
        /// <returns>Результат выполнения операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}