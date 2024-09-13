using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PRODUCTSERVICE.API.Dtos
{
  public class ArtistReadDto
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public string Avatar { get; set; }
    public string Biography { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
  }

  public class ArtistBriefReadDto
  {
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Avatar { get; set; }
    public string Biography { get; set; }
  }

  public class ArtistCreateDto
  {
    [Required]
    [RegularExpression("^([A-Za-z ])+$", ErrorMessage = "Only contain alphabet and space characters.")]
    public string FirstName { get; set; }
    [Required]
    [RegularExpression("^([A-Za-z ])+$", ErrorMessage = "Only contain alphabet and space characters.")]
    public string LastName { get; set; }
    [Required]
    [RegularExpression("^([a-zA-Z0-9_]){3,}$", ErrorMessage = "Only Alphabet or Number or \"_\" is accepted. Minumum length is 3.")]
    public string Nickname { get; set; }
    [Required]
    public IFormFile Avatar { get; set; }
    public string Biography { get; set; }
    [Required]
    [Phone]
    [MinLength(9)]
    [MaxLength(12)]
    public string Phone { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Url]
    public string Instagram { get; set; }
    [Url]
    public string Facebook { get; set; }
  }
}