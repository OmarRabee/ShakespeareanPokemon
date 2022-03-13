using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShakespeareanPokemon.Domain.Models;

namespace ShakespeareanPokemon.Api.Dependencies
{
   public static class SettingsDependency
   {
      public static PokemonSettings RegisterPokemonSettings(this IServiceCollection services, IConfiguration configuration)
      {
         var pokemonSettings = configuration.GetSection("PokemonSettings");
         services.Configure<PokemonSettings>(pokemonSettings);
         return pokemonSettings.Get<PokemonSettings>();
      }
   }
}