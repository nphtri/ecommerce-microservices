using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("banners")]
  public class Banner
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("description", TypeName = "nvarchar(200)")]
    public string Description { get; set; }

    [Column("image", TypeName = "varchar(200)")]
    [Required]
    public string Image { get; set; }

    [Column("is_active")]
    [Required]
    public bool IsActive { get; set; }

    [Column("order_index")]
    [Required]
    public int OrderIndex { get; set; }

    [Column("target_url", TypeName = "varchar(200)")]
    public string TargetUrl { get; set; }

    [Column("created_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime CreatedTime { get; set; }

    [Column("modified_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime ModifiedTime { get; set; }
  }
}