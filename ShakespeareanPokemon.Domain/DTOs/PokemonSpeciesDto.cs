using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.DTOs
{
   public class PokemonSpeciesDto
   {
      [JsonPropertyName("name")] public string Name { get; set; }

      [JsonPropertyName("form_descriptions")] public FormDescription[] FormDescriptions { get; set; }
   }
   public class FormDescription
   {
      [JsonPropertyName("description")] public string Description { get; set; }
      [JsonPropertyName("language")] public Language Language { get; set; }
   }

   public class Language
   {
      [JsonPropertyName("name")] public string Name { get; set; }
   }
}
