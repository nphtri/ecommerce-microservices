using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Helpers;
using PRODUCTSERVICE.API.Services;

namespace PRODUCTSERVICE.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ArtsController : ControllerBase
  {
    private readonly IArtService _artService;
    private readonly IArtistService _artistService;

    public ArtsController(IArtService artService, IArtistService artistService)
    {
      _artService = artService;
      _artistService = artistService;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<ArtReadDto>> GetArts()
    {
      return Ok(_artService.GetAllArts());
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetArtById")]
    public ActionResult<ArtReadDto> GetArtById(int id)
    {
      var result = _artService.GetArtById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return NotFound();
    }

    [Authorize(Roles = "ARTIST")]
    [HttpPost]
    public ActionResult<ArtReadDto> CreateArt([FromForm] ArtCreateDto dto)
    {
      var artist = _artistService.GetArtistByEmail(UserHelpers.GetUserEmail(User));
      if (artist == null)
      {
        return NotFound(new { Message = "Artist is not existed." });
      }
      var result = _artService.CreateArt(dto, artist.Id);
      if (result != null)
      {
        return CreatedAtRoute(nameof(GetArtById), new { Id = result.Id }, result);
      }
      return BadRequest();
    }

    #region art-styles 

    [AllowAnonymous]
    [HttpGet("styles")]
    public ActionResult<IEnumerable<ArtStyleDto>> GetStyles()
    {
      return Ok(_artService.GetArtStyles());
    }

    #endregion
  }
}