using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.DTOs.Requests
{
   public class TranslateRequest
   {
      [JsonPropertyName("text")] public string Text { get; set; }
   }
}
