using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("products")]
  public class Product
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("art_id")]
    [ForeignKey("Art")]
    public int? ArtId { get; set; }
    public Art Art { get; set; }

    [Column("name", TypeName = "nvarchar(500)")]
    [Required]
    public string Name { get; set; }

    [Column("description", TypeName = "nvarchar(MAX)")]
    [Required]
    public string Description { get; set; }

    [Column("type_id")]
    [ForeignKey("ProductType")]
    [Required]
    public int TypeId { get; set; }
    public Lookup ProductType { get; set; }

    [Column("price")]
    [Required]
    public double Price { get; set; }

    [Column("image", TypeName = "varchar(200)")]
    [Required]
    public string Image { get; set; }

    [Column("created_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime CreatedTime { get; set; }

    [Column("modified_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime ModifiedTime { get; set; }
  }
}