using Microsoft.Extensions.DependencyInjection;
using ShakespeareanPokemon.Domain.Models;
using System;

namespace ShakespeareanPokemon.Api.Dependencies
{
   public static class HttpClientDependency
   {
      public static void RegisterHttpClient(this IServiceCollection services, PokemonSettings pokemonSettings)
      {
         services.AddHttpClient("PokemonClient", httpClient =>
         {
            httpClient.BaseAddress = new Uri(pokemonSettings.PoekmonApiBase);
         });
         services.AddHttpClient("ShakespeareTranslatorClient", httpClient =>
         {
            httpClient.BaseAddress = new Uri(pokemonSettings.ShakespeareanTranslationApiBase);
         });
      }
   }
}