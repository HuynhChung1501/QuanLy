using System.ComponentModel.DataAnnotations;

namespace QuanLy.Application.DTO.Sprints
{
    public class SprintDTO
    {
        public int SprintID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int ProjectID { get; set; }
        public byte IsShow { get; set; }
        public int TaskCount { get; set; }
        public decimal Progress { get; set; }
    }

    public class SprintDTOCreate
    {
        [Required, MaxLength(250)] public string Name { get; set; } = string.Empty;
        [MaxLength(2000)] public string? Goal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        [Range(1, int.MaxValue)] public int ProjectID { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class SprintDTOUpdate : SprintDTOCreate
    {
        [Range(1, int.MaxValue)] public int SprintID { get; set; }
        public byte IsShow { get; set; } = 1;
        public int? UpdatedBy { get; set; }
    }

    public class SprintDTOParam
    {
        public string? Keyword { get; set; }
        public int? ProjectID { get; set; }
        public int? Status { get; set; }
        [Range(1, int.MaxValue)] public int PageIndex { get; set; } = 1;
        [Range(1, 200)] public int PageSize { get; set; } = 20;
    }
}
