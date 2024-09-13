using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Helpers;
using PRODUCTSERVICE.API.Services;

namespace PRODUCTSERVICE.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ArtistsController : ControllerBase
  {
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
      _artistService = artistService;
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetArtistById")]
    public ActionResult<ArtistReadDto> GetArtistById(int id)
    {
      var artist = _artistService.GetArtistById(id);
      if (artist != null)
      {
        if (!User.IsInRole("ADMIN"))
        {
          if (UserHelpers.GetUserEmail(User) != artist.Email)
          {
            return Forbid();
          }
        }
        return Ok(artist);
      }
      return NotFound();
    }

    [Authorize]
    [HttpGet("my-info")]
    public ActionResult<ArtistReadDto> GetArtistByToken()
    {
      var artist = _artistService.GetArtistByEmail(UserHelpers.GetUserEmail(User));
      if (artist != null)
      {
        return Ok(artist);
      }
      return NotFound();
    }

    [Authorize(Roles = "ARTIST")]
    [HttpPost]
    public ActionResult<ArtistReadDto> CreateArtist([FromForm] ArtistCreateDto dto)
    {
      if (UserHelpers.GetUserEmail(User) != dto.Email)
      {
        return BadRequest(new { Message = "Email is invalid." });
      }
      if (UserHelpers.GetUserPhone(User) != dto.Phone)
      {
        return BadRequest(new { Message = "Phone number is invalid." });
      }
      if (_artistService.GetArtistByEmail(dto.Email) != null)
      {
        return Conflict(new { Message = "Email is existed" });
      }
      if (_artistService.GetArtistByPhone(dto.Email) != null)
      {
        return Conflict(new { Message = "Phone is existed" });
      }
      if (_artistService.GetArtistByNickname(dto.Nickname) != null)
      {
        return Conflict(new { Message = "Nickname is existed" });
      }

      var result = _artistService.CreateArtist(dto);
      if (result != null)
      {
        return CreatedAtRoute(nameof(GetArtistById), new
        {
          Id = result.Id
        }, result);
      }
      return BadRequest();
    }

    #region brief 

    [AllowAnonymous]
    [HttpGet("brief")]
    public ActionResult<IEnumerable<ArtistBriefReadDto>> GetArtistsBrief()
    {
      return Ok(_artistService.GetArtistsBrief());
    }

    [AllowAnonymous]
    [HttpGet("brief/{id}")]
    public ActionResult<ArtistBriefReadDto> GetArtistBrief(int id)
    {
      var brief = _artistService.GetArtistBriefById(id);
      if (brief != null)
      {
        return Ok(brief);
      }
      return NotFound();
    }

    #endregion
  }
}