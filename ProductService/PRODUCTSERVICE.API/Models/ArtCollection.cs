using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("art_collections")]
  public class ArtCollection
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("name", TypeName = "nvarchar(200)")]
    [Required]
    public string Name { get; set; }

    [Column("publisher", TypeName = "nvarchar(200)")]
    [Required]
    public string Publisher { get; set; }

    [Column("published_date", TypeName = "datetime2(7)")]
    [Required]
    public DateTime PublishedDate { get; set; }

    [Column("image", TypeName = "varchar(200)")]
    [Required]
    public string Image { get; set; }

    [Column("description", TypeName = "nvarchar(MAX)")]
    [Required]
    public string Description { get; set; }

    [Column("created_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime CreatedTime { get; set; }

    [Column("modified_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime ModifiedTime { get; set; }

    public ICollection<Art> Arts { get; set; }
  }
}