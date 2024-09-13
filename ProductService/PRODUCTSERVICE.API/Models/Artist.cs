using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.API.Models
{
  [Table("artists")]
  public class Artist
  {
    [Column("id")]
    [Key]
    [Required]
    public int Id { get; set; }

    [Column("first_name", TypeName = "nvarchar(200)")]
    [Required]
    public string FirstName { get; set; }
    [Column("last_name", TypeName = "nvarchar(200)")]
    [Required]
    public string LastName { get; set; }

    [Column("nickname", TypeName = "nvarchar(200)")]
    public string Nickname { get; set; }

    [Column("avatar", TypeName = "varchar(200)")]
    [Required]
    public string Avatar { get; set; }

    [Column("biography", TypeName = "nvarchar(MAX)")]
    public string Biography { get; set; }

    [Column("phone", TypeName = "varchar(15)")]
    [Required]
    public string Phone { get; set; }

    [Column("email", TypeName = "varchar(30)")]
    [Required]
    public string Email { get; set; }

    [Column("instagram", TypeName = "varchar(50)")]
    public string Instagram { get; set; }

    [Column("facebook", TypeName = "varchar(50)")]
    public string Facebook { get; set; }

    [Column("created_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime CreatedTime { get; set; }

    [Column("modified_time", TypeName = "datetime2(7)")]
    [Required]
    public DateTime ModifiedTime { get; set; }

    public ICollection<Art> Arts { get; set; }
  }
}