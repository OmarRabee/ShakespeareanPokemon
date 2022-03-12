using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.DTOs.Responses
{
   public class TranslateResponse
   {
      [JsonPropertyName("contents")] public Contents Contents { get; set; }
   }

   public class Contents
   {
      [JsonPropertyName("translated")] public string TranslatedText { get; set; }
   }
}
