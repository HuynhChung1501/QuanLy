using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Domain.Models
{
    [Table("ProjectTasks")]
    public class ProjectTask
    {
        [Key]
        public int TaskID { get; set; }

        [Required, MaxLength(250)]
        public required string Title { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        public int ProjectID { get; set; }
        public int? SprintID { get; set; }
        public int? AssigneeID { get; set; }
        public int? ReporterID { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }
        public byte IsShow { get; set; } = 1;
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Project? Project { get; set; }
        public Sprint? Sprint { get; set; }
    }
}
