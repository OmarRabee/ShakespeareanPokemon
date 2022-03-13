using ShakespeareanPokemon.Domain.Interfaces.ApiHandlers;
using ShakespeareanPokemon.Service.ApiHandlers;

namespace ShakespeareanPokemon.Api.Dependencies
{
   public static class ApiHandlerDependency
   {
      public static void RegisterApiHadndlers(this IServiceCollection services)
      {
         services.AddScoped<IPokemonApiHandler, PokemonApiHandler>();
      }
   }
}
