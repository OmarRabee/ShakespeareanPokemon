using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.Models;

namespace ShakespeareanPokemon.Domain.Interfaces.Services
{
   public interface IPokemonService
   {
      Task<ServiceResult<PokemonDto>> GetPokemonAsync(string name);
   }
}
