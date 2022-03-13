using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.DTOs.Responses;

namespace ShakespeareanPokemon.Domain.Interfaces.ApiHandlers
{
   public interface IPokemonApiHandler
   {
      Task<PokemonSpeciesDto> GetPokemonSpeciesAsync(string name);
      Task<TranslateResponse> GetShakespereanTranslation(string textToBeTranslated);
   }
}
