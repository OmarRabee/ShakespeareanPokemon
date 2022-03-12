using Serilog;
using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.DTOs.Requests;
using ShakespeareanPokemon.Domain.DTOs.Responses;
using ShakespeareanPokemon.Domain.Enums;
using ShakespeareanPokemon.Domain.Interfaces.Services;
using ShakespeareanPokemon.Domain.Models;
using System.Text;
using System.Text.Json;

namespace ShakespeareanPokemon.Service
{
   public class PokemonService : IPokemonService
   {
      private readonly IHttpClientFactory _httpClientFactory;
      private readonly HttpClient _pokemonClient;
      private readonly HttpClient _translationClient;
      private readonly ILogger _logger;

      public PokemonService(IHttpClientFactory httpClientFactory, ILogger logger)
      {
         _httpClientFactory = httpClientFactory;
         _pokemonClient = _httpClientFactory.CreateClient("PokemonClient");
         _translationClient = _httpClientFactory.CreateClient("ShakespeareTranslatorClient");
         _logger = logger;
      }

      public async Task<ServiceResult<PokemonDto>> GetPokemonAsync(string name)
      {
         try
         {
            var pokemonSpecies = await GetPokemonSpeciesAsync(name);
            var translatedDescription = await GetShakespereanTranslation(pokemonSpecies.FormDescriptions.FirstOrDefault(d => d.Language.Name == "en")?.Description);
            var pokemon = new PokemonDto() { Name = pokemonSpecies.Name, Description = translatedDescription?.Contents?.TranslatedText };
            return new ServiceResult<PokemonDto>(pokemon);
         }
         catch (Exception ex)
         {
            _logger.Error(ex.Message);
            return new ServiceResult<PokemonDto>(new ErrorResult(PokemonError.ErrorGettingPokemon, ex.Message));
         }

      }

      private async Task<PokemonSpeciesDto> GetPokemonSpeciesAsync(string name)
      {
         var pokemonSpecies = await _pokemonClient.GetAsync($"pokemon-species/{name}");
         var pokemonSpeciesStr = await pokemonSpecies.Content.ReadAsStringAsync();
         return JsonSerializer.Deserialize<PokemonSpeciesDto>(pokemonSpeciesStr);
      }

      private async Task<TranslateResponse> GetShakespereanTranslation(string textToBeTranslated)
      {
         var request = new TranslateRequest() { Text = textToBeTranslated };
         var data = new StringContent(JsonSerializer.Serialize<TranslateRequest>(request), Encoding.UTF8, "application/json");
         var response = await _translationClient.PostAsync("", data);
         var translatedResponseStr = await response.Content.ReadAsStringAsync();
         return JsonSerializer.Deserialize<TranslateResponse>(translatedResponseStr);
      }
   }
}