using AutoMapper;
using PokemonApi.DataAccess.Entities;
using PokemonApi.Models.PokemonDto;

namespace PokemonApi.Profiles
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
