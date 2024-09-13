using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Models
{
    [Table("accounts")]
    public class Account
    {
        [Column("id")]
        [Key]
        [Required]
        public int Id { get; set; }
        [Column("username", TypeName = "varchar(200)")]
        [Required]
        public string Username { get; set; }
        [Column("hashed", TypeName = "varchar(150)")]
        [Required]
        public string Hashed { get; set; }
        [Column("email", TypeName = "nvarchar(30)")]
        [Required]
        public string Email { get; set; }
        [Required]
        [Column("phone", TypeName = "varchar(15)")]
        public string Phone { get; set; }
        [Column("failed_accessed_times")]
        [Required]
        public int FailedAccessTimes { get; set; } = 0;
        [Column("is_email_verified")]
        [Required]
        public bool IsEmailVerified { get; set; } = false;
        [Column("is_phone_verified")]
        [Required]
        public bool IsPhoneVerified { get; set; } = false;
        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Column("last_accessed", TypeName = "datetime2(7)")]
        public DateTime? LastAccessed { get; set; } = null;
        [Column("created_time", TypeName = "datetime2(7)")]
        [Required]
        public DateTime CreatedTime { get; set; }
        [Column("modified_time", TypeName = "datetime2(7)")]
        [Required]
        public DateTime ModifiedTime { get; set; }
        [Column("role_id")]
        [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}