using ShakespeareanPokemon.Domain.Extensions;
using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.Models
{
   public class ErrorResult
   {
      public ErrorResult()
      {
      }

      public ErrorResult(Enum type, params string[] values)
      {
         Type = type;
         if (values.Length > 0)
            ErrorMessage = string.Format(type.GetDescription(), values);
         else
            ErrorMessage = string.Format(type.GetDescription(), "");

      }

      public ErrorResult(int id, string propertyName, string message)
      {
         Id = id;
         PropertyName = propertyName;
         ErrorMessage = message;
      }

      [JsonPropertyName("id")]
      public int Id { get; set; }

      [JsonIgnore]
      public Enum Type { get; set; }

      [JsonPropertyName("propertyName")]
      public string PropertyName { get; set; }

      [JsonPropertyName("errorMessage")]
      public string ErrorMessage { get; set; }
   }
}

