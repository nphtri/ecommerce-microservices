using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("arts")]
  public class Art
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("name", TypeName = "nvarchar(200)")]
    [Required]
    public string Name { get; set; }

    [Column("artist_id")]
    [ForeignKey("Artist")]
    [Required]
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }

    [Column("style_id")]
    [ForeignKey("ArtStyle")]
    [Required]
    public int StyleId { get; set; }
    public Lookup ArtStyle { get; set; }

    [Column("description", TypeName = "nvarchar(MAX)")]
    [Required]
    public string Description { get; set; }

    [Column("short", TypeName = "nvarchar(500)")]
    public string Short { get; set; }

    [Column("image", TypeName = "varchar(200)")]
    [Required]
    public string Image { get; set; }

    [Column("collection_id")]
    [ForeignKey("ArtCollection")]
    public int? CollectionId { get; set; }
    public ArtCollection Collection { get; set; }

    [Column("created_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime CreatedTime { get; set; }

    [Column("modified_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime ModifiedTime { get; set; }

    public ICollection<Product> Products { get; set; }
  }
}