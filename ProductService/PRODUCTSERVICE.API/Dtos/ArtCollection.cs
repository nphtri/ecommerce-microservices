using System;

namespace PRODUCTSERVICE.API.Dtos
{
  public class ArtCollectionReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Publisher { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
  }

  public class ArtCollectionBriefReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}