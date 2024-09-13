using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("lookup_types")]
  public class LookupType
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("value", TypeName = "nvarchar(200)")]
    [Required]
    public string Value { get; set; }

    public ICollection<Lookup> Lookups { get; set; }
  }
}