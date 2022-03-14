using Microsoft.Extensions.Options;
using Serilog;
using ShakespeareanPokemon.Domain.Enums;
using ShakespeareanPokemon.Domain.Extensions;
using ShakespeareanPokemon.Domain.Models;
using ShakespeareanPokemon.Service.ApiHandlers;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ShakespeareanPokemon.Service.IntegrationTests.PokemonServiceIntegrationTests
{
   public static class TestConstants
   {
      public const string DescriptionLanguage = "en";
      public const string PoekmonApiBase = "https://pokeapi.co/api/v2/";
      public const string ShakespeareanTranslationApiBase = "https://api.funtranslations.com/translate/shakespeare";
   }

   public class GetPokemonAsyncIntegrationTests
   {
      static PokemonSettings pokemonSettings = new PokemonSettings()
      {
         DescriptionLanguage = TestConstants.DescriptionLanguage,
         PoekmonApiBase = TestConstants.PoekmonApiBase,
         ShakespeareanTranslationApiBase = TestConstants.ShakespeareanTranslationApiBase
      };
      readonly static IOptions<PokemonSettings> pokemonSettingsOptions = Options.Create(pokemonSettings);
      private static readonly ILogger logger = Log.ForContext<GetPokemonAsyncIntegrationTests>();
      static PokemonApiHandler pokemonApiHandler = new PokemonApiHandler(new TestableHttpClientFactory());
      readonly PokemonService pokemonService = new PokemonService(pokemonApiHandler, pokemonSettingsOptions, logger);

      [Fact]
      public async Task GetPokemon_ValidPokemonName_ReturnsPokemonWithTranslatedDescription()
      {
         // Arrange
         var validPokemonName = "wormadam";

         // Act
         var actualResult = await pokemonService.GetPokemonAsync(validPokemonName);

         // Assert
         Assert.True(actualResult.Success);
         Assert.Equal(actualResult.Result.Name, validPokemonName);
      }

      [Fact]
      public async Task GetPokemon_InvalidPokemonName_ReturnsInvalidPokemonNamen()
      {
         // Arrange
         var invalidPokemonName = "someInvalidPokemonName";

         // Act
         var actualResult = await pokemonService.GetPokemonAsync(invalidPokemonName);

         // Assert
         Assert.False(actualResult.Success);
         Assert.Contains(actualResult.Errors, e => e.ErrorMessage == PokemonError.InvalidPokemonName.GetDescription());

      }
   }

   public class TestableHttpClientFactory : IHttpClientFactory
   {
      public HttpClient CreateClient(string name)
      {
         var httpClient = new HttpClient();
         httpClient.BaseAddress = name == "PokemonClient"
             ? new System.Uri(TestConstants.PoekmonApiBase)
             : new System.Uri(TestConstants.ShakespeareanTranslationApiBase);
         return httpClient;
      }
   }
}
