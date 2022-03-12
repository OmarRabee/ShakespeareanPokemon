using System.Text.Json.Serialization;

namespace ShakespeareanPokemon.Domain.Models
{
   public abstract class ServiceResult
   {
      [JsonPropertyName("success")]
      public bool Success { get; set; }

      [JsonPropertyName("errors")]
      public List<ErrorResult> Errors { get; set; }
   }

   public class ServiceResult<TResult> : ServiceResult
   {
      [JsonPropertyName("result")]
      public TResult Result { get; set; }


      public ServiceResult()
           : this(success: true, result: default, errors: null)
      {

      }

      public ServiceResult(TResult result)
          : this(success: true, result: result, errors: null)
      { }

      public ServiceResult(ErrorResult error)
          : this(success: false, result: default, errors: new List<ErrorResult>() { error })
      {
      }

      public ServiceResult(List<ErrorResult> errors)
          : this(success: false, result: default, errors: errors)
      {
      }

      public ServiceResult(bool success, TResult result, List<ErrorResult> errors)
      {
         Success = success;
         Result = result;
         Errors = errors;
      }
   }
}
