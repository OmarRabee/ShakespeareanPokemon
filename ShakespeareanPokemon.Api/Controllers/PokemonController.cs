using Microsoft.AspNetCore.Mvc;
using ShakespeareanPokemon.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace ShakespeareanPokemon.Api.Controllers
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class PokemonController : ControllerBase
   {
      public IPokemonService _pokemonService { get; set; }

      public PokemonController(IPokemonService pokemonService)
      {
         _pokemonService = pokemonService;
      }

      [HttpGet("{name}")]
      public async Task<IActionResult> GetPokemon(string name)
      {
         var result = await _pokemonService.GetPokemonAsync(name);
         return Ok(result);
      }
   }
}
