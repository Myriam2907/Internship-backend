using System.ComponentModel.DataAnnotations;

namespace Internship_backend.Models;

public class Projects
{
    [Key] public int ProjectId { get; set; }

    // Project name
    [Required] [MaxLength(100)] public string ProjectName { get; set; }

    // Client ID
    [Required] public int ClientId { get; set; }
}