using Microsoft.Extensions.DependencyInjection;
using ShakespeareanPokemon.Domain.Interfaces.Services;
using ShakespeareanPokemon.Service;

namespace ShakespeareanPokemon.Api.Dependencies
{
   public static class ServiceDependency
   {
      public static void RegisterServices(this IServiceCollection services)
      {
         services.AddScoped<IPokemonService, PokemonService>();
      }
   }
}
