using Moq;
using Serilog;
using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.DTOs.Responses;
using ShakespeareanPokemon.Domain.Interfaces.ApiHandlers;
using System.Threading.Tasks;
using Xunit;
namespace ShakespeareanPokemon.Service.UnitTests.PokemonServiceTests
{

   public class GetPokemonAsyncTests
   {
      private readonly Mock<IPokemonApiHandler> _pokemonApiHandlerMock = new Mock<IPokemonApiHandler>();
      private readonly Mock<ILogger> _logger = new Mock<ILogger>();

      PokemonService _pokemonService;

      #region Constants
      static Language SOME_LANGUAGE = new Language() { Name = "SOME_LANG_NAME" };
      static FormDescription SOME_FORM_DESCRIPTION = new FormDescription() { Description = "SOME_DESCRIPTION", Language = SOME_LANGUAGE };
      static string SOME_TRNSLATED_TEXT = "SOME_TRANSLATED_TEXT";
      static string SOME_POKE_NAME = "SOME_POKE_NAME";
      TranslateResponse SOME_TRANSLATE_RESPONSE = new TranslateResponse() { Contents = new Contents() { TranslatedText = SOME_TRNSLATED_TEXT } };
      PokemonSpeciesDto SOME_POKEMON_SPECIES = new PokemonSpeciesDto() { Name = SOME_POKE_NAME, FormDescriptions = new FormDescription[1] { SOME_FORM_DESCRIPTION } };
      #endregion
      public GetPokemonAsyncTests()
      {
         _pokemonService = new PokemonService(_pokemonApiHandlerMock.Object, _logger.Object);
      }

      [Fact]
      public async Task GetPokemon_Success_ReturnsPokemonWithTranslatedDescription()
      {
         // Arrange
         _pokemonApiHandlerMock.Setup(m => m.GetPokemonSpeciesAsync(It.IsAny<string>())).ReturnsAsync(SOME_POKEMON_SPECIES);
         _pokemonApiHandlerMock.Setup(m => m.GetShakespereanTranslation(It.IsAny<string>())).ReturnsAsync(SOME_TRANSLATE_RESPONSE);

         // Act
         var actualResult = await _pokemonService.GetPokemonAsync(SOME_POKE_NAME);

         // Assert
         Assert.Equal(actualResult.Result.Name, SOME_POKE_NAME);
      }
   }
}
