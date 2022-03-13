# ShakespeareanPokemon
A simple API for getting a pokemon with its shakespearean description, it's built on .NET 6
it utilies two RESTful APIs:
1. RESTful Pokémon API - https://pokeapi.co/
2. Shakespeare translator API - https://funtranslations.com/api/shakespeare

# Requirements:
1. .NET 6

# Nuget Packages used:
* xunit 2.4.1
* Moq 4.17.2
* Serilog.AspNetCore 5.0.0
* NSwag.AspNetCore 13.15.10

# How to run:
1. Clone the repo.
2. Go to this path `ShakespeareanPokemon\ShakespeareanPokemon.Api` and run this command `dotnet run`
3. The API will be up and running after this locally on port `7060` you can use this path in the browser: `https://localhost:7060/swagger/index.html`

# Try the API:
The API contains one endpoint which recieves a pokemon name, and returns the pokemon name and the shakespearean translation for it. to try the API.
1. Go to `https://localhost:7060/swagger/index.html`.
2. Click on `try it out`
3. Pass any pokemon name, for example: `wormadam`
4. Press Execute
5. A reponse will be sent back:
`{
  "result": {
    "name": "wormadam",
    "description": "Forms hath't different stats and movepools. During evolution,  burmy's current cloak becomes wormadam's form,  and can nay longer beest did doth change."
  },
  "success": true,
  "errors": null
}`
![image](https://user-images.githubusercontent.com/11810466/158057208-8e2edd45-78b0-4555-9289-982dff0199f6.png)

# Unit testing
To run the unit tests for this project you can run it directly from VS or Rider, or you can navigate to this path: `ShakespeareanPokemon\ShakespeareanPokemon.Service.UnitTests` and run this command `dotnet test`
