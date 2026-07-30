using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Domain.Models
{
    [Table("Sprints")]
    public class Sprint
    {
        [Key]
        public int SprintID { get; set; }

        [Required, MaxLength(250)]
        public required string Name { get; set; }

        [MaxLength(2000)]
        public string? Goal { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int ProjectID { get; set; }
        public byte IsShow { get; set; } = 1;
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Project? Project { get; set; }
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
