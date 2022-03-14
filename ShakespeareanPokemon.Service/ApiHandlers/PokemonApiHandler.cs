using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.DTOs.Requests;
using ShakespeareanPokemon.Domain.DTOs.Responses;
using ShakespeareanPokemon.Domain.Interfaces.ApiHandlers;
using System.Text;
using System.Text.Json;

namespace ShakespeareanPokemon.Service.ApiHandlers
{
   public class PokemonApiHandler : IPokemonApiHandler
   {
      private readonly HttpClient _pokemonClient;
      private readonly HttpClient _translationClient;

      public PokemonApiHandler(IHttpClientFactory httpClientFactory)
      {
         _pokemonClient = httpClientFactory.CreateClient("PokemonClient");
         _translationClient = httpClientFactory.CreateClient("ShakespeareTranslatorClient");
      }

      public async Task<PokemonSpeciesDto> GetPokemonSpeciesAsync(string name)
      {
         try
         {
            var pokemonSpecies = await _pokemonClient.GetAsync($"pokemon-species/{name}");
            var pokemonSpeciesStr = await pokemonSpecies.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PokemonSpeciesDto>(pokemonSpeciesStr) ?? new PokemonSpeciesDto();
         }
         catch (Exception ex)
         {
            return new PokemonSpeciesDto();
         }
      }

      public async Task<TranslateResponse> GetShakespeareanTranslation(string textToBeTranslated)
      {
         try
         {
            var request = new TranslateRequest() { Text = textToBeTranslated };
            var data = new StringContent(JsonSerializer.Serialize<TranslateRequest>(request), Encoding.UTF8, "application/json");
            var response = await _translationClient.PostAsync("", data);
            var translatedResponseStr = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TranslateResponse>(translatedResponseStr) ?? new TranslateResponse();
         }
         catch (Exception ex)
         {
            return new TranslateResponse();
         }
      }
   }
}
