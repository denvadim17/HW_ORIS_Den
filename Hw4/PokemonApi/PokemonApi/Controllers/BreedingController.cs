using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.DataAccess;
using PokemonApi.DataAccess.Entities;
using PokemonApi.Models.BreedingDto;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class BreedingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BreedingController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для получения веса и роста
        /// </summary>
        /// <returns>Возращает список веса и роста всех покемонов</returns>
        [HttpGet]
        public async Task<IEnumerable<Breeding>> GetAllBreedings()
        {
            return await _context.Breedings.ToListAsync();
        }
        
        /// <summary>
        /// Метод для получения веса и роста по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор веса и роста</param>
        /// <returns>Возвращает вес и рост покемона с указанным идентификатором</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<Breeding>> GetByIdBreeding([FromRoute]int id)
        {
            return await _context.Breedings.FindAsync(id) ?? throw new Exception();
        }
        
        /// <summary>
        /// Метод для создания нового покемона
        /// </summary>
        /// <param name="breeding">Данные новых роста и веса</param>
        /// <returns>Возвращает новые рост и вес</returns>
        [HttpPost]
        public async Task<ActionResult<BreedingCreateDto>> PostBreeding(BreedingCreateDto breeding)
        {
            var newBreeding = new Breeding()
            {
                Height = breeding.Height,
                Weight = breeding.Weight,
            };

            _context.Breedings.Add(newBreeding);
            await _context.SaveChangesAsync();

            return breeding;
        }

        /// <summary>
        /// Метод для обновления роста и веса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="breeding"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreeding([FromRoute] int id, BreedingUpdateDto breeding)
        {
            var breedings = await _context.Breedings.FindAsync(id);
            if(breedings == null)
            {
                return NotFound();
            }

            breedings.Height = breeding.Height;
            breedings.Weight = breeding.Weight;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Метод для удаления роста и веса по их идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreeding([FromRoute] int id)
        {
            var breeding = await _context.Breedings.FindAsync(id);
            if (breeding == null)
            {
                return NotFound();
            }

            _context.Breedings.Remove(breeding);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
