namespace ShakespeareanPokemon.Api.Middlewares
{
   public class ErrorHandlingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly Serilog.ILogger logger;

      public ErrorHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
      {
         _next = next;
         this.logger = logger;
      }

      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _next(context);
         }
         catch (Exception e)
         {
            logger.Error(e, e.Message);
            throw;
         }
      }
   }
}
