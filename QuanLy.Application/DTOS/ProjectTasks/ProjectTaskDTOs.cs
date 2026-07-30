using System.ComponentModel.DataAnnotations;

namespace QuanLy.Application.DTO.ProjectTasks
{
    public class ProjectTaskDTO
    {
        public int TaskID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProjectID { get; set; }
        public int? SprintID { get; set; }
        public int? AssigneeID { get; set; }
        public int? ReporterID { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }
        public byte IsShow { get; set; }
    }

    public class ProjectTaskDTOCreate
    {
        [Required, MaxLength(250)] public string Title { get; set; } = string.Empty;
        [MaxLength(4000)] public string? Description { get; set; }
        [Range(1, int.MaxValue)] public int ProjectID { get; set; }
        public int? SprintID { get; set; }
        public int? AssigneeID { get; set; }
        public int? ReporterID { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class ProjectTaskDTOUpdate : ProjectTaskDTOCreate
    {
        [Range(1, int.MaxValue)] public int TaskID { get; set; }
        public byte IsShow { get; set; } = 1;
        public int? UpdatedBy { get; set; }
    }

    public class ProjectTaskDTOParam
    {
        public string? Keyword { get; set; }
        public int? ProjectID { get; set; }
        public int? SprintID { get; set; }
        public int? AssigneeID { get; set; }
        public int? Priority { get; set; }
        public int? Status { get; set; }
        [Range(1, int.MaxValue)] public int PageIndex { get; set; } = 1;
        [Range(1, 200)] public int PageSize { get; set; } = 20;
    }
}
