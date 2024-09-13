using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Dtos
{
  public class ArtReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ArtistBriefReadDto Artist { get; set; }
    public string Style { get; set; }
    public string Description { get; set; }
    public string Short { get; set; }
    public string Image { get; set; }
    public ArtCollectionBriefReadDto Collection { get; set; }
  }

  public class ArtStyleDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }

  public class ArtCreateDto
  {
    [Required]
    [RegularExpression("^[A-Za-z0-9 -]+$")]
    public string Name { get; set; }
    [Required]
    public IFormFile Image { get; set; }
    [Range(1, int.MaxValue)]
    public int? CollectionId { get; set; }
    [Range(1, 1000)]
    public int StyleId { get; set; }
    public string Description { get; set; }
    public string Short { get; set; }
  }
}