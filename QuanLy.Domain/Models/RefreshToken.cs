using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Domain.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string TokenHash { get; set; }

        public DateTime ExpireAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedAt { get; set; }

        public string? CreatedByIp { get; set; }

        public string? RevokedByIp { get; set; }

        public bool IsRevoked { get; set; }
    }
}
