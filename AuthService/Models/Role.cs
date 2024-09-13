using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models
{
    [Table("roles")]
    public class Role
    {
        [Column("id")]
        [Key]
        [Required]
        public int Id { get; set; }
        [Column("name", TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}