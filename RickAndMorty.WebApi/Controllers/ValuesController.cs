using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.WebApi.Context;
using RickAndMorty.WebApi.DTOs;

namespace RickAndMorty.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController(HttpClient httpClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsingRickAndMortyAPI()
    {
        var response = await httpClient.GetAsync("https://rickandmortyapi.com/api/episode");//Api isteği attım
        EpisodeDto? episodeDto = await response.Content.ReadFromJsonAsync<EpisodeDto>();//Gelen response'u EpisodeDto'ya çevirdim

        return Ok(episodeDto);
    }
}
