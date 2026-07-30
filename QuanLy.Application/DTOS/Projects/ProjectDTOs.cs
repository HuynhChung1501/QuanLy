using System.ComponentModel.DataAnnotations;

namespace QuanLy.Application.DTO.Projects
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int ProjectManagerID { get; set; }
        public byte IsShow { get; set; }
        public int SprintCount { get; set; }
        public int TaskCount { get; set; }
        public decimal Progress { get; set; }
    }

    public class ProjectDTOCreate
    {
        [Required, MaxLength(50)] public string Code { get; set; } = string.Empty;
        [Required, MaxLength(250)] public string Name { get; set; } = string.Empty;
        [MaxLength(2000)] public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int ProjectManagerID { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class ProjectDTOUpdate : ProjectDTOCreate
    {
        [Range(1, int.MaxValue)] public int ProjectID { get; set; }
        public byte IsShow { get; set; } = 1;
        public int? UpdatedBy { get; set; }
    }

    public class ProjectDTOParam
    {
        public string? Keyword { get; set; }
        public int? Status { get; set; }
        public int? ProjectManagerID { get; set; }
        [Range(1, int.MaxValue)] public int PageIndex { get; set; } = 1;
        [Range(1, 200)] public int PageSize { get; set; } = 20;
    }
}
