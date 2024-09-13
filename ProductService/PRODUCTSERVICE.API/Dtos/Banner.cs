using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PRODUCTSERVICE.API.Dtos
{
  public class BannerReadDto
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
    public int OrderIndex { get; set; }
    public string TargetUrl { get; set; }
  }

  public class BannerCreateDto
  {
    public string Description { get; set; }
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    [Range(1, 10)]
    public int OrderIndex { get; set; }
    public string TargetUrl { get; set; }
  }
}