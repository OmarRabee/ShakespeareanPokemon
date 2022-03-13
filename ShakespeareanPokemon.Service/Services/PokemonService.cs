using Microsoft.Extensions.Options;
using Serilog;
using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.Enums;
using ShakespeareanPokemon.Domain.Interfaces.ApiHandlers;
using ShakespeareanPokemon.Domain.Interfaces.Services;
using ShakespeareanPokemon.Domain.Models;

namespace ShakespeareanPokemon.Service
{
   public class PokemonService : IPokemonService
   {
      private readonly ILogger _logger;
      private readonly IPokemonApiHandler _pokemonApiHandler;
      private readonly PokemonSettings _pokemonSettings;

      public PokemonService(IPokemonApiHandler pokemonApiHandler, IOptions<PokemonSettings> pokemonSettings, ILogger logger)
      {
         _pokemonApiHandler = pokemonApiHandler;
         _pokemonSettings = pokemonSettings.Value;
         _logger = logger;
      }

      public async Task<ServiceResult<PokemonDto>> GetPokemonAsync(string name)
      {
         try
         {
            var pokemonSpecies = await _pokemonApiHandler.GetPokemonSpeciesAsync(name);
            if (string.IsNullOrEmpty(pokemonSpecies.Name))
               return new ServiceResult<PokemonDto>(new ErrorResult(PokemonError.InvalidPokemonName));
            if (!pokemonSpecies.FormDescriptions.Any(d => d?.Language?.Name == _pokemonSettings.DescriptionLanguage))
               return new ServiceResult<PokemonDto>(new ErrorResult(PokemonError.NoEnglishDescriptionFound));

            var translatedDescription = await _pokemonApiHandler.GetShakespeareanTranslation(pokemonSpecies.FormDescriptions.FirstOrDefault(d => d?.Language?.Name == _pokemonSettings.DescriptionLanguage)?.Description);
            PokemonDto? pokemon = new PokemonDto() { Name = pokemonSpecies.Name, Description = translatedDescription?.Contents?.TranslatedText };
            return new ServiceResult<PokemonDto>(pokemon);
         }
         catch (Exception ex)
         {
            _logger.Error(ex.Message);
            return new ServiceResult<PokemonDto>(new ErrorResult(PokemonError.ErrorGettingPokemon, ex.Message));
         }
      }
   }
}