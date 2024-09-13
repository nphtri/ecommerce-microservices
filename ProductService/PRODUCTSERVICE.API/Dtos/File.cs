using Microsoft.AspNetCore.Http;

namespace PRODUCTSERVICE.API.Dtos
{
  public class FileDto
  {
    public IFormFile FormFile { get; set; }
  }
}