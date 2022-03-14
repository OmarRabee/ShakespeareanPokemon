using Moq;
using Serilog;
using ShakespeareanPokemon.Domain.DTOs;
using ShakespeareanPokemon.Domain.DTOs.Responses;
using ShakespeareanPokemon.Domain.Enums;
using ShakespeareanPokemon.Domain.Extensions;
using ShakespeareanPokemon.Domain.Interfaces.ApiHandlers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShakespeareanPokemon.Service.UnitTests.PokemonServiceUnitTests
{
   public class GetPokemonAsyncUnitTests
   {
      private readonly Mock<IPokemonApiHandler> _pokemonApiHandlerMock = new Mock<IPokemonApiHandler>();
      private readonly Mock<ILogger> _logger = new Mock<ILogger>();

      PokemonService _pokemonService;

      #region Constants
      static Language SOME_LANGUAGE = new Language() { Name = "en" };
      static FormDescription SOME_FORM_DESCRIPTION = new FormDescription() { Description = "SOME_DESCRIPTION", Language = SOME_LANGUAGE };
      static string SOME_TRNSLATED_TEXT = "SOME_TRANSLATED_TEXT";
      static string SOME_POKE_NAME = "SOME_POKE_NAME";

      TranslateResponse SOME_TRANSLATE_RESPONSE = new TranslateResponse() { Contents = new Contents() { TranslatedText = SOME_TRNSLATED_TEXT } };
      PokemonSpeciesDto SOME_POKEMON_SPECIES = new PokemonSpeciesDto() { Name = SOME_POKE_NAME, FormDescriptions = new FormDescription[1] { SOME_FORM_DESCRIPTION } };
      #endregion
      public GetPokemonAsyncUnitTests()
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
         Assert.True(actualResult.Success);
         Assert.Equal(actualResult.Result.Name, SOME_POKE_NAME);
      }

      [Fact]
      public async Task GetPokemon_EmptyName_ReturnsInvalidPokemonName()
      {
         // Arrange
         var pokemonSpecies = new PokemonSpeciesDto() { Name = "", FormDescriptions = new FormDescription[0] };
         _pokemonApiHandlerMock.Setup(m => m.GetPokemonSpeciesAsync(It.IsAny<string>())).ReturnsAsync(pokemonSpecies);

         // Act
         var actualResult = await _pokemonService.GetPokemonAsync(SOME_POKE_NAME);

         // Assert
         Assert.False(actualResult.Success);
         Assert.True(actualResult.Errors.Any(e => e.ErrorMessage == PokemonError.InvalidPokemonName.GetDescription()));
      }

      [Fact]
      public async Task GetPokemon_NoEnglishDescription_ReturnsNoEnglishDescriptionFound()
      {
         // Arrange
         var pokemonSpecies = new PokemonSpeciesDto() { Name = "SOME_NAME", FormDescriptions = new FormDescription[0] };
         _pokemonApiHandlerMock.Setup(m => m.GetPokemonSpeciesAsync(It.IsAny<string>())).ReturnsAsync(pokemonSpecies);

         // Act
         var actualResult = await _pokemonService.GetPokemonAsync(SOME_POKE_NAME);

         // Assert
         Assert.False(actualResult.Success);
         Assert.True(actualResult.Errors.Any(e => e.ErrorMessage == PokemonError.NoEnglishDescriptionFound.GetDescription()));
      }

      [Fact]
      public async Task GetPokemon_ThrowsException_LogExceptionAndReturnsError()
      {
         // Arrange
         _pokemonApiHandlerMock.Setup(m => m.GetPokemonSpeciesAsync(It.IsAny<string>())).Throws(new System.Exception("SOME_EXCEPTION_MESSAGE"));

         // Act
         var actualResult = await _pokemonService.GetPokemonAsync(SOME_POKE_NAME);

         // Assert
         _logger.Verify(m => m.Error(It.IsAny<string>()), Times.Once);
         Assert.False(actualResult.Success);
         Assert.True(actualResult.Errors.Any(e => e.ErrorMessage == PokemonError.ErrorGettingPokemon.GetDescription()));
      }
   }
}
