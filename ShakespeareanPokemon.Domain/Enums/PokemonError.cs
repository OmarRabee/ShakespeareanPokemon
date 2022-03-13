using System.ComponentModel;

namespace ShakespeareanPokemon.Domain.Enums
{
   public enum PokemonError
   {
      [Description("Failure while getting the pokemon")]
      ErrorGettingPokemon,

      [Description("Pokemon species not found")]
      PokemonSpeciesNotFound,

      [Description("Invalid Pokemon name")]
      InvalidPokemonName,

      [Description("No description found")]
      NoEnglishDescriptionFound
   }
}
