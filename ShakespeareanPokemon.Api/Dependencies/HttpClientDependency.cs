namespace ShakespeareanPokemon.Api.Dependencies
{
   public static class HttpClientDependency
   {
      public static void RegisterHttpClient(this IServiceCollection services)
      {
         services.AddHttpClient("PokemonClient", httpClient =>
         {
            httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
         });
         services.AddHttpClient("ShakespeareTranslatorClient", httpClient =>
         {
            httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/shakespeare");
         });
      }
   }
}