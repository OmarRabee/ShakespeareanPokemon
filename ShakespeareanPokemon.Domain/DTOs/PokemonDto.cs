using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.DTOs
{
   public class PokemonDto
   {
      [JsonPropertyName("name")] public string Name { get; set; }
      [JsonPropertyName("description")] public string Description { get; set; }
   }
}
