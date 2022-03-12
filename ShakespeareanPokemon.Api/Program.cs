using Serilog;
using ShakespeareanPokemon.Api.Dependencies;
using ShakespeareanPokemon.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterServices();
builder.Services.RegisterHttpClient();
builder.Services.AddOpenApiDocument();
builder.Services.AddHealthChecks();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Error");
   app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
   app.UseOpenApi();
   app.UseSwaggerUi3();
   app.UseReDoc();
}

app.UseHealthChecks("/health");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseRouting();

app.Run();