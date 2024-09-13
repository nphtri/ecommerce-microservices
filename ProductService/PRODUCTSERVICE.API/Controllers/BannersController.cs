using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Services;

namespace PRODUCTSERVICE.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BannersController : ControllerBase
  {
    private readonly IBannerService _bannerService;

    public BannersController(IBannerService bannerService)
    {
      _bannerService = bannerService;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<BannerReadDto>> GetBanners()
    {
      return Ok(_bannerService.GetBanners());
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetBannerById")]
    public ActionResult<BannerReadDto> GetBannerById(int id)
    {
      var result = _bannerService.GetBannerById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return NotFound();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    public ActionResult<BannerReadDto> CreateBanner([FromForm] BannerCreateDto dto)
    {
      var result = _bannerService.CreateBanner(dto);
      if (result != null)
      {
        return CreatedAtAction(nameof(GetBannerById), new { Id = result.Id }, result);
      }
      return BadRequest();
    }
  }
}