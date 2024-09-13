using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("lookups")]
  public class Lookup
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("value", TypeName = "nvarchar(200)")]
    [Required]
    public string Value { get; set; }

    [Column("lookup_type_id")]
    [ForeignKey("LookupType")]
    [Required]
    public int LookupTypeId { get; set; }
    public LookupType LookupType { get; set; }

    [InverseProperty("ArtStyle")]
    public ICollection<Art> ArtsByStyle { get; set; }

    [InverseProperty("ProductType")]
    public ICollection<Product> ProductsByType { get; set; }
  }
}